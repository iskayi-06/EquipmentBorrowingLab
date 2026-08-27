using EquipmentBorrowing.Application.Services;
using EquipmentBorrowing.Domain;
using EquipmentBorrowing.Infrastructure.Repositories;

Console.WriteLine("--- Campus Equipment Borrowing System ---\n");

var studentRepo = new InMemoryStudentRepository();
var equipmentRepo = new InMemoryEquipmentRepository();
var borrowingRepo = new InMemoryBorrowingRepository();


studentRepo.Seed(new Student { Id = 1, Name = "Alice", IsAllowedToBorrow = true, ActiveBorrowingsCount = 0 });
studentRepo.Seed(new Student { Id = 2, Name = "Bob", IsAllowedToBorrow = false, ActiveBorrowingsCount = 0 });


equipmentRepo.Seed(new Equipment { Id = 101, Name = "Oscilloscope", IsAvailable = true });


var service = new BorrowEquipmentService(studentRepo, equipmentRepo, borrowingRepo);


Console.WriteLine("[Scenario 1: Successful Borrowing]");
Console.WriteLine("Action: Alice requests the Oscilloscope...");
await service.BorrowAsync(studentId: 1, equipmentId: 101, daysToBorrow: 3);


Console.WriteLine("\n[Scenario 2: Failure - Equipment Unavailable]");
Console.WriteLine("Action: Another student tries to borrow the Oscilloscope Alice just took...");
await service.BorrowAsync(studentId: 1, equipmentId: 101, daysToBorrow: 2);


Console.WriteLine("\n[Scenario 3: Failure - Student Not Allowed]");
Console.WriteLine("Action: Bob tries to borrow equipment...");
equipmentRepo.Seed(new Equipment { Id = 102, Name = "Microscope", IsAvailable = true });
await service.BorrowAsync(studentId: 2, equipmentId: 102, daysToBorrow: 1);