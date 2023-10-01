select top 1 * from dbo.plnmolddf order by plan_no desc

select  * from spreadmf where parent_item_cd like '8905'

select ITEM_CODE, ITEM_NAME from productmf where classify like '4'

select item_code, use_rubber from productmf where item_code like '8905'

select plan_no as [Plan], pln.ITEM_CODE as [Item Code], (plan_qty * prod.USE_RUBBER) as [Rubber Qty], sp.comp_item as [Rubber Code]
from plnmolddf as pln
join productmf as prod on prod.ITEM_CODE = pln.ITEM_CODE
join (select PARENT_ITEM_CD, left(COMP_ITEM_CD,5) as comp_item from Spreadmf where COMP_ITEM_CD LIKE 'R%') as sp on sp.PARENT_ITEM_CD=pln.ITEM_CODE
where pln.PLAN_NO like '219%'

select pln.ITEM_CODE as [Item Code], SUM (pls.plan_qty * prod.USE_RUBBER) as [Rubber Qty], sp.comp_item as [Rubber Code]
from plnshiftdf as pls
join plnmolddf as pln on pln.PLAN_NO = pls.PLAN_NO
join productmf as prod on prod.ITEM_CODE = pln.ITEM_CODE
join (select PARENT_ITEM_CD, left(COMP_ITEM_CD,5) as comp_item from Spreadmf where COMP_ITEM_CD LIKE 'R%') as sp on sp.PARENT_ITEM_CD=pln.ITEM_CODE
where (pls.PLAN_DATE between '2021-09-20' and '2021-09-25') and pln.MC_NO not like 'MI%' and pln.INV_DV not like '*'
GROUP BY pln.ITEM_CODE, SP.comp_item
order by pln.ITEM_CODE

select pln.*, pln.ITEM_CODE, pln.MC_NO, pln.PLAN_QTY, pls.PLAN_QTY, pls.PLAN_DATE
from plnmolddf as pln
join plnshiftdf as pls on pln.PLAN_NO = pls.PLAN_NO
where pls.PLAN_DATE between '2021-09-20' and '2021-09-25' and pln.ITEM_CODE in ('7919', '7911')

select pln.ITEM_CODE as [Item Code], SUM (pls.plan_qty * prod.USE_RUBBER) as [Rubber Qty], sp.comp_item as [Rubber Code]
from gplnshiftdf as pls
join gplnmolddf as pln on pln.PLAN_NO = pls.PLAN_NO
join productmf as prod on prod.ITEM_CODE = pln.ITEM_CODE
join (select PARENT_ITEM_CD, left(COMP_ITEM_CD,5) as comp_item from Spreadmf where COMP_ITEM_CD LIKE 'R%') as sp on sp.PARENT_ITEM_CD=pln.ITEM_CODE
where pls.PLAN_DATE between '2021-09-20' and '2021-09-25'
GROUP BY pln.ITEM_CODE, SP.comp_item
order by pln.ITEM_CODE

(select top 1 COMP_ITEM_CD, prod.ITEM_NAME from Spreadmf as spr
JOIN productmf as prod on prod.ITEM_CODE = left(spr.COMP_ITEM_CD, 5)
WHERE PARENT_ITEM_CD LIKE '1517' and COMP_ITEM_CD LIKE 'R%')

select prod.ITEM_CODE, prod.ITEM_NAME from productmf as prod where prod.ITEM_CODE like 'R3118'