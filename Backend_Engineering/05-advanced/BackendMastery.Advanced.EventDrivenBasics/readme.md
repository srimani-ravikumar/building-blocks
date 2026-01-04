# 🔴 Event‑Driven Basics — RabbitMQ

> **Concept‑first, failure‑aware introduction to event‑driven systems**

This project demonstrates **why event‑driven architecture exists**, not how to wire frameworks together.

It models the **minimum viable event‑driven boundary** using RabbitMQ and .NET 8 — deliberately avoiding abstractions that hide failure modes.

---

## 📌 What problem does event‑driven architecture solve?

In real production systems, doing everything synchronously causes:

* Slow user responses
* Cascading failures across services
* Tight coupling between teams
* Poor behavior under traffic spikes

HTTP chains create **temporal coupling** (everything must succeed *now*) and **dependency coupling** (everyone must be healthy).

Event‑driven architecture exists to **break both**.

---

## 🧠 Intuition (plain English)

> Some work must happen immediately.
> Some work only needs to happen eventually.

Event‑driven systems separate:

* **Intent** (handled synchronously via HTTP)
* **Consequences** (handled asynchronously via events)

A service publishes a **fact that already happened**.
Other parts of the system decide whether they care.

The producer does **not wait**.
The consumer does **not block** the producer.

---

## 🌍 Real‑world use case

### Order placement

When a user places an order:

Must happen synchronously:

* Validate request
* Accept order
* Respond quickly

Does *not* need to block the response:

* Send confirmation email
* Update analytics
* Trigger shipping workflow
* Write audit logs

### Without events (bad)

```
Order API
 ├── Email API
 ├── Analytics API
 └── Shipping API
```

One failure → user waits or request fails.

### With events (good)

```
Order API
 └── OrderPlaced event → RabbitMQ
      ├── Email consumer
      ├── Analytics consumer
      └── Shipping consumer
```

Fast responses. Isolated failures. Independent scaling.

---

## ❌ What breaks without events?

Naive synchronous systems suffer from:

* User latency caused by non‑critical work
* Retry storms during partial outages
* One service deployment breaking others
* Inability to scale features independently

The system becomes **fragile under load**.

---

## ⚠️ Common misconceptions

❌ *“Events replace HTTP”*
→ No. Commands are still synchronous.

❌ *“RabbitMQ means microservices”*
→ No. Events work inside monoliths too.

❌ *“Fire‑and‑forget is unsafe”*
→ True **without** observability, idempotency, and retries.

Events reduce coupling — they do not remove responsibility.

---

## 🧩 How this concept transfers across stacks

Event‑driven thinking is **language‑agnostic**.

| Stack | Event Mechanism              |
| ----- | ---------------------------- |
| .NET  | RabbitMQ / Azure Service Bus |
| Java  | Kafka / RabbitMQ             |
| Node  | NATS / Kafka                 |
| Go    | Pub/Sub / Streams            |

The transport changes.
The mental model does not.

---

## 🧱 Project Structure

```
BackendMastery.Advanced.EventDrivenBasics/
│
├── Controllers/
│   └── OrdersController.cs          # Synchronous intent (HTTP)
│
├── Contracts/
│   └── Events/
│       └── OrderPlacedEvent.cs      # Immutable fact
│
├── Services/
│   └── OrderService.cs              # Core business logic
│
├── Infrastructure/
│   └── Messaging/
│       ├── RabbitMqConnection.cs    # Connection lifecycle
│       ├── EventPublisher.cs        # Event emission
│       └── EventConsumer.cs         # Asynchronous processing
│
├── Program.cs
└── appsettings.json
```

### Boundary enforced

> HTTP handles **intent**.
> Events handle **consequences**.

---

## 🛠️ Key design decisions

* **Official RabbitMQ.Client only** — no abstractions
* **Async‑first APIs** (RabbitMQ.Client v7+)
* **Explicit connection and channel management**
* **Single queue** to keep the concept focused

Each decision favors **visibility of failure modes** over convenience.

---

## 🧪 What this project deliberately does NOT include

* Manual acknowledgements
* Idempotent consumers
* Retry policies
* Dead‑letter queues

These are intentionally deferred.

This project teaches **why events exist**, not how to harden them yet.

---

## 🚨 Common mistakes in real systems

* Publishing events inside database transactions
* Treating events as commands
* Blocking HTTP requests on consumers
* Assuming consumers will only run once

These mistakes re‑introduce coupling and failure propagation.

---

## ✅ What you should take away

After this project, you should be able to:

* Explain **why event‑driven architecture exists**
* Identify **when HTTP breaks down**
* Design async workflows without over‑engineering
* Re‑implement the same idea in any backend stack

This project is intentionally small — because **clarity precedes scale**.

---