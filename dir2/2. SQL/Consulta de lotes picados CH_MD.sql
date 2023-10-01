SELECT      dateadd(DAY,0, datediff(day,0, END_TIME)) as Dia, COUNT (LOT_NO) as Total, substring(LOT_NO,2,4) as Item
FROM        PRODIRECTDF
WHERE       (DATEDIFF(MONTH, END_TIME, '2021-02-01') = 0)AND (PRO_CODE = 'MD') AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 5  AND (substring(ITEM_CODE,1,1) like '7' OR substring(ITEM_CODE,6,1) like 'H')))
GROUP by    DATEADD(dd, 0, DATEDIFF(dd, 0, END_TIME)), substring(LOT_NO,2,4)
ORDER by	DATEADD(dd, 0, DATEDIFF(dd, 0, END_TIME))
