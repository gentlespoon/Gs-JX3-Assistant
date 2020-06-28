<?php
// 2020-06-14T13:43:44-07:00
// Angda Song
// JX3 Life Skill Assistant - Server

if (count($PATH) == 1 || $PATH[1] == '') {
  throw new Exception('No API specified.');
}

if (!is_file(APIROOT.'/'.$PATH[0].'/'.$PATH[1].'.php')) {
  throw new Exception('Invalid API');
}

// Connect to database
$datasources = ['mysql'];
require_once(APIROOT.'/'.$PATH[0].'/_gitignored.db_config.php');

require_once(APIROOT.'/'.$PATH[0].'/'.$PATH[1].'.php');
