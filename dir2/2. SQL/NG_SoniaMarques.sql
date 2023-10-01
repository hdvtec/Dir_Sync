select top 1 g.LOT_NO, w.SCAN_TIME, g.PRO_CODE, g.START_TIME, g.END_TIME, s.LAST_QTY from proworkdf as w
join PRODIRECTDF as g on w.DIRECT_NO = g.DIRECT_NO join shiagelotdf as s on s.LOT_NO = g.LOT_NO 
where w.mc_no like '' order by scan_time desc

select top 10 * from proworkdf
where scan_dv like '4'
order by data_no desc

select top 1 g.LOT_NO, w.SCAN_TIME, g.PRO_CODE, g.START_TIME, g.END_TIME, s.LAST_QTY 
from proworkdf as w
join PRODIRECTDF as g on w.DIRECT_NO = g.DIRECT_NO 
join shiagelotdf as s on s.LOT_NO = g.LOT_NO

select PRO_CODE as Process, sum(NG_QTY) as NG
from gprodirectdf 
where start_time between '2021/09/01 00:00:00' and '2021/09/30 23:59:59'
and PRO_CODE in ('MD', 'KE')
group by pro_code

--CH
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 5  AND (substring(ITEM_CODE,1,1) like '7' OR substring(ITEM_CODE,6,1) like 'H' OR substring(ITEM_CODE,1,4) IN ('3305','3306'))))
GROUP by    PRO_CODE

--IN-02
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 5  AND (substring(ITEM_CODE,1,4) IN ('3503','3505', '3801', '3802', '3602'))))
GROUP by    PRO_CODE

--INJ-150
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        GPRODIRECTDF as g
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL')) 
			AND (substring(LOT_NO,3,1) like '4')
GROUP by    PRO_CODE

--MA-3
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL', 'LI')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 1  AND (substring(ITEM_CODE,1,1) like '2' OR substring(ITEM_CODE,1,4) IN ('3701','3502', '3506','3508','3304'))))
GROUP by    PRO_CODE

--MP-80
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        GPRODIRECTDF as g
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL')) 
			AND (substring(LOT_NO,3,1) like '5')
GROUP by    PRO_CODE

--PL-93
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL', 'LI')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 1  AND (substring(ITEM_CODE,1,1) like '1')))
GROUP by    PRO_CODE

--PV200
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'GR', 'SL', 'LI')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 1  AND (substring(ITEM_CODE,1,1) like '6')))
GROUP by    PRO_CODE

--MGCH
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'ASS')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 5  AND (substring(ITEM_CODE,1,1) like '8' AND substring(ITEM_CODE,6,1) not like 'H' and substring(ITEM_CODE,1,4) 
			IN ('8200', '8201', '8301', '8601', '8602', '8614', '8701', '8702', '8703', '8704', '8709', '8711', '8714', '8717', '8718', '8719', '8720', '8910', '8912', '8914', '8916'))))
GROUP by    PRO_CODE

--MGL
SELECT      PRO_CODE, sum(PRO_QTY + NG_QTY) as [Total Produced], sum(NG_QTY) as NG
FROM        PRODIRECTDF
WHERE       (END_TIME between '2021-09-01 00:00:00' and '2021-09-30 23:59:59') AND (PRO_CODE in ('MD', 'KE', 'ASS')) AND (substring(LOT_NO,2,4) in 
			(Select substring(ITEM_CODE,1,4) from PRODUCTMF where 
			Category_no = 5  AND (substring(ITEM_CODE,1,1) like '8' AND substring(ITEM_CODE,6,1) not like 'H' and substring(ITEM_CODE,1,4) 
			NOT IN ('8200', '8201', '8301', '8601', '8602', '8614', '8701', '8702', '8703', '8704', '8709', '8711', '8714', '8717', '8718', '8719', '8720', '8910', '8912', '8914', '8916'))))
GROUP by    PRO_CODE