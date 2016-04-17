<?php
$data = array();
$data['error']=0;

$ini_config = parse_ini_file('Config.ini');

$db = new mysqli($ini_config['hostname'],$ini_config['account'],$ini_config['password'],$ini_config['database']);

if(!$db)
{
	$data['error']=1;

}
echo json_encode($data);
?>