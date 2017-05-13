/*Script para Restaurar Base de datos desde archivo .bak*/
/* el archivo que se baja desde duplica es .bak aunque no tiene extencion*/

/*^Primero corremos esto y vemos que teien el backup*/
--RESTORE FILELISTONLY
--FROM DISK = 'C:\Users\caria\Desktop\AG Fotografia WebConfig Bien\AGfotografia_2017-01-29_13-04-33.bak'


RESTORE DATABASE AGfotografia FROM DISK = 'C:\Users\caria\Desktop\AG Fotografia WebConfig Bien\AGfotografia_2017-01-29_13-04-33.bak'
WITH MOVE 'AGfotografia' TO 'C:\Users\caria\Source\Repos\AGFotografia\AGfotografia.mdf',
MOVE 'AGfotografia_log' TO 'C:\Users\caria\Source\Repos\AGFotografia\AGfotografia_log.ldf';

