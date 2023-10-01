select * from prodirectdf where lot_no like 'S61012280003-050'
select * from proworkdf where direct_no like '1227659'
select * from prostockdf where lot_no like 'S61012280003-050'
select * from prolotdf where item_lot_no like 'FI6101220912-08'

update prodirectdf set PRO_QTY = 1000 where direct_no like '1227656'
update proworkdf set QUANTITY = 1357 where data_no like '2276345'
delete prostockdf where lot_no = 'S61012280005-046' and box_no = 2
update prolotdf set ITEM_QUANTITY = 726 where data_no like '996386'

select * from sql_log where sql_string like '%S61012280003-050%'