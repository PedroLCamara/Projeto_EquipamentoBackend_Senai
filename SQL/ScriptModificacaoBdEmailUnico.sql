USE Patrimonio;   
GO  
ALTER TABLE usuarios   
ADD CONSTRAINT email_unico UNIQUE (email);   
GO  
