<?php
// 2020-06-14T13:43:44-07:00
// Angda Song
// JX3 Life Skill Assistant - Server - AllItems

$rs_allItems = DB::query("SELECT * FROM items WHERE Available=1");

writeResponse(1, $rs_allItems);