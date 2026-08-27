using EquipmentBorrowing.Application.Services;
using EquipmentBorrowing.Domain;
using EquipmentBorrowing.Infrastructure.Repositories;

Console.WriteLine("--- Campus Equipment Borrowing System ---\n");

// 1. Setup Infrastructure (In-Memory Repositories)
var studentRepo = new InMemoryStudentRepository();
var equipmentRepo = new InMemoryEquipmentRepository();
var borrowingRepo = new InMemoryBorrowingRepository();

// 2. Seed Mock Data
// Alice is allowed to borrow. Bob is NOT allowed to borrow.
studentRepo.Seed(new Student { Id = 1, Name = "Alice", IsAllowedToBorrow = true, ActiveBorrowingsCount = 0 });
studentRepo.Seed(new Student { Id = 2, Name = "Bob", IsAllowedToBorrow = false, ActiveBorrowingsCount = 0 });

// The Oscilloscope is available.
equipmentRepo.Seed(new Equipment { Id = 101, Name = "Oscilloscope", IsAvailable = true });

// 3. Setup Application Service (Manual Dependency Injection)
// We pass the repositories into the service via the constructor, fulfilling Part F.
var service = new BorrowEquipmentService(studentRepo, equipmentRepo, borrowingRepo);


// 4. Successful Case[cite: 1]
Console.WriteLine("[Scenario 1: Successful Borrowing]");
Console.WriteLine("Action: Alice requests the Oscilloscope...");
await service.BorrowAsync(studentId: 1, equipmentId: 101, daysToBorrow: 3);


// 5. Failure Case 1 - Equipment Unavailable[cite: 1]
Console.WriteLine("\n[Scenario 2: Failure - Equipment Unavailable]");
Console.WriteLine("Action: Another student tries to borrow the Oscilloscope Alice just took...");
await service.BorrowAsync(studentId: 1, equipmentId: 101, daysToBorrow: 2);


// 6. Failure Case 2 - Student Not Allowed[cite: 1]
Console.WriteLine("\n[Scenario 3: Failure - Student Not Allowed]");
Console.WriteLine("Action: Bob tries to borrow equipment...");
// We will seed a new available item for this test
equipmentRepo.Seed(new Equipment { Id = 102, Name = "Microscope", IsAvailable = true });
await service.BorrowAsync(studentId: 2, equipmentId: 102, daysToBorrow: 1);