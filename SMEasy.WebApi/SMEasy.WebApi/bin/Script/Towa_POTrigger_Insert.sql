CREATE TRIGGER TRG_PurchaseOrderHistory_Insert    
ON PurchaseOrder    
FOR INSERT     
AS    
    
 DECLARE @PurchaseOrderId bigint;    
 DECLARE @ExpectedDeliveryDate Datetime;    
 DECLARE @Notes [nvarchar](max);      
 DECLARE @CreationUserId [nchar](50);    
    
SELECT     
@PurchaseOrderId =i.Id,     
@ExpectedDeliveryDate = i.ExpectedDeliveryDate,    
@Notes = i.ExpectedDeliveryDateNotes,     
@CreationUserId= i.CreationUserId    
FROM inserted i;    
   
INSERT INTO PurchaseOrderHistory(PurchaseOrderId,ExpectedDeliveryDate,  
 Notes,CreationTs,CreationUserId, TriggerFor, StatusType) VALUES(@PurchaseOrderId, @ExpectedDeliveryDate, @Notes,    
 GETDATE(),@CreationUserId , 'INSERT', 1);    
 