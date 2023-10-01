SELECT
    sp.comp_item_cd as [Anilha/Slinger], sp.Parent_item_cd as [Produto], dt.Repetitions
    FROM Spreadmf as sp
        INNER JOIN ( SELECT comp_item_cd, COUNT(*) AS Repetitions FROM Spreadmf
						WHERE SUBSTRING(Comp_item_cd,1,1) in ('I','L')
                        GROUP BY comp_item_cd
                        HAVING COUNT(comp_item_cd)>1
                    ) as dt ON sp.comp_item_cd=dt.comp_item_cd
	WHERE (SUBSTRING(Parent_item_cd,1,1) like '8' 
OR (SUBSTRING(Parent_item_cd,1,1) like '7' AND SUBSTRING(Parent_item_cd,LEN(Parent_item_cd) ,1) not like ')'))
	ORDER BY SP.Comp_item_cd