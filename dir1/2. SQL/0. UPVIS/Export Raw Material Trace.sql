USE UPVIS

insert into gLanguage VALUES
('PMRMTMSG000','Ficheiro em uso','File in use', 'File in use'),
('PMRMTMSG001','Guardado com sucesso!','Saved successfully!', 'Saved successfully!'),
('PMRMTMSG002','Guardado como: ','Saved as: ', 'Saved as: '),
('PMRMTTLT001','Guardar','Save', 'Save'),
('PMRMTTLT002','Erro','Error', 'Error'),
('PMRMTCMD001','Exportar','Export','Export')

insert into SqlQuerys values
('PMRMT000','SELECT process.PROCESS_NAME as Process, CAST(P.Pro_Qty as int) FROM {0} as P 
LEFT JOIN (select process_code, process_name from processmf) as process on process.PROCESS_CODE=P.pro_code 
WHERE P.LOT_NO like ''{1}'' ORDER BY P.DIRECT_NO DESC'),
('PMRMT001','SELECT Defect_Name, CAST(NG_Qty as int) FROM {0} where (LOT_NO LIKE ''{1}'') and process_no in 
(select P.Pro_Code from PRODIRECTDF as P WHERE P.LOT_NO like ''{2}'')')

USE UPV

select top 10 * from PROLOTDF order by DATA_NO desc

SELECT process.PROCESS_NAME as Process, CAST(P.Pro_Qty as int)
FROM PRODIRECTDF
as P LEFT JOIN (select process_code, process_name from processmf) as process on process.PROCESS_CODE=P.pro_code
WHERE P.LOT_NO like 'S25342290042-008' ORDER BY P.DIRECT_NO DESC

SELECT Defect_Name, CAST(NG_Qty  as int) 
FROM DEFPRODF
where (LOT_NO LIKE 'S73012330234-033') 
and process_no in (select P.Pro_Code from PRODIRECTDF as P WHERE P.LOT_NO like 'S73012330234-033')

SELECT Lot.Item_Code, Item_Lot_No, Item_Quantity, Mat_Lot_No  + '-' + right('000' + cast(iod.BOX_NO as varchar(3)), 3) as [Mat_Lot_No], CAST(0 as bit) as Export 
FROM PROLOTDF as LOT LEFT JOIN PRODUCTMF as Prod on Prod.ITEM_CODE = LOT.ITEM_CODE left join IODATADF as iod on iod.SO_TO_KHAI = ITEM_LOT_NO 
WHERE MAT_CODE like '%L%' and MAT_LOT_NO like '%%' ORDER BY lot.DATA_NO DESC