CREATE TRIGGER TRG_PurchaseOrderHistory_Update             
ON PurchaseOrder          
FOR       
    UPDATE            
AS            
    SET NOCOUNT ON             
 DECLARE @PurchaseOrderId bigint;            
 DECLARE @ExpectedDeliveryDate Datetime;            
 DECLARE @Notes [nvarchar](max);              
 DECLARE @CreationUserId [nchar](50);            
                    
    IF UPDATE(ExpectedDeliveryDate)            
        BEGIN            
            /*             
            this verifies that the column actually changed in value.  As a IF UPDATE() will return true if you execute             
            a statement LIKE this UPDATE TABLE SET COLUMN = COLUMN which is not a change.            
            */            
   IF EXISTS(SELECT 1 FROM inserted I JOIN deleted D ON I.Id = D.Id AND I.ExpectedDeliveryDate <> D.ExpectedDeliveryDate)            
       BEGIN            
        SELECT                
@PurchaseOrderId =i.Id,             
@ExpectedDeliveryDate = i.ExpectedDeliveryDate,            
@Notes = i.ExpectedDeliveryDateNotes,             
@CreationUserId= i.LastChangeUserId            
FROM inserted i;   
  
INSERT INTO PurchaseOrderHistory(PurchaseOrderId,ExpectedDeliveryDate,          
 Notes,CreationTs,CreationUserId, TriggerFor, StatusType) VALUES(@PurchaseOrderId, @ExpectedDeliveryDate, @Notes,            
 GETDATE(),@CreationUserId , 'UPDATE', 1);              
       END            
END 