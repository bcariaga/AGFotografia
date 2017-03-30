SELECT  'VALUES(' + CAST(A.ID AS VARCHAR) + ' , ' 
			+ '''' + A.Titulo + ''''+ ',' 
			+ ''''+ A.Tags + '''' + ','
			+ ''''+ A.Portada + '''' +  ')'
				FROM Albunes A

SET IDENTITY_INSERT dbo.Albunes ON;  
INSERT INTO Albunes (ID, Titulo, Tags, Portada)
	VALUES	(1030 , 'Black & Grey','el clasico preferido','https://fbcdn-sphotos-a-a.akamaihd.net/hphotos-ak-xaf1/t31.0-8/11731737_1037247906300396_3949409216967662270_o.jpg') ,
			(1031 , 'Vida Salvaje','naturaleza','https://scontent-mia1-1.xx.fbcdn.net/hphotos-xpf1/t31.0-8/12273524_1108385212519998_5853612791485292740_o.jpg') ,
			(1035 , 'Thomas','modelo','https://scontent-mia1-1.xx.fbcdn.net/hphotos-xaf1/t31.0-8/10947417_1002949513063569_679830595610619500_o.jpg') ,
			(1039 , 'Fotografia de Producto','Estudio Brunna Gaia - Whorkshop con Susana Mutti','https://scontent-mia1-1.xx.fbcdn.net/hphotos-xpt1/t31.0-8/s960x960/12819300_1179449655413553_3116749991795412239_o.jpg') ,
			(1041 , 'NAHIR','BOOK EN EXTERIOR','https://fbcdn-sphotos-d-a.akamaihd.net/hphotos-ak-xfa1/v/t1.0-9/11083626_976602835698237_8902993406189242863_n.jpg?oh=fef5c3aa0fd5b50bf3cc458049bef42d&oe=578414E8&__gda__=1467978718_c8e040e565e2b895f79e2cca4fb39823') ,
			(1042 , 'TATTOO SOLIDARIO','JORNADA POR LOS ANIMALES','https://scontent-gru2-1.xx.fbcdn.net/hphotos-xtp1/v/t1.0-9/10993397_947794028579118_7134397185909223302_n.jpg?oh=479dde9a573a80544d9f57c2daccc914&oe=57B81904') ,
			(1043 , 'isla san andres, colombia','mis vacaciones','https://scontent.fgig1-2.fna.fbcdn.net/v/t1.0-9/13450273_1255150701176781_6999656117668211516_n.jpg?oh=7f315b3b852ae9817a5b78c36ddf0600&oe=580AD91D') ,
			(1044 , 'Book estudio','nahir scardamaglia','https://scontent.fgig1-1.fna.fbcdn.net/v/t1.0-9/13654419_1275838039108047_747906515153316964_n.jpg?oh=fff9578f6ad64e7609e8f683b27a66a8&oe=5843B941') 
			SET IDENTITY_INSERT dbo.Albunes OFF; 