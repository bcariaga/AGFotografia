/*Script para Restaurar Base de datos desde archivo .bak*/
/* el archivo que se baja desde duplica es .bak aunque no tiene extencion*/

/*^Primero corremos esto y vemos que teien el backup*/
--RESTORE FILELISTONLY
--FROM DISK = 'C:\Users\caria\Desktop\AGfotografia_2017-01-29_13-04-33.bak'


RESTORE DATABASE AGfotografia
FROM DISK = 'C:\Users\caria\Desktop\AGfotografia_2017-01-29_13-04-33.bak'


/*aca va el nombre de la db qu esta en el achivo y el destino donde se guarda la db*/
WITH MOVE 'AGfotografia' TO 'C:\Users\caria\Documents\Proyectos\AG fotografia\AGFotografia\SQL\AGFotografia.mdf',
MOVE 'AGfotografia_log' TO 'C:\Users\caria\Documents\Proyectos\AG fotografia\AGFotografia\SQL\AGFotografia.ldf',
REPLACE;

SELECT * FROM Contacto