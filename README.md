# MassTransit ActiveMQ Issue

Repro for MassTransit ActiveMQ Temporary Queue issue.

1. Verify the number of ActiveMQ queues.
2. `dotnet run`
3. Two new queues should exist.
4. &lt;CTRL&gt;-C
5. One of the queues will still exist.
6. Repeat 1-4 and one new queue will be left each time.
7. Eventually ActiveMQ will reach the maximum number of queues.
