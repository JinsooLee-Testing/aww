<?php
include 'lib.php';

$id = $_POST['id'];
$password = $_POST['password'];
$hash = $_POST['hash'];

$sql = query("SELECT password FROM userinfo WHERE id = '$id'");
$row = mysqli_fetch_row($sql);
$phash = $row[0];

if(password_verify($password, $phash)){
	
	$sql2 = query("UPDATE userinfo SET hash = '$hash' WHERE id = '$id'");
	$row = mysqli_fetch_row($sql2);
	echo ("loginsucceed");

}else{

	echo ("incorrect");
	
}
/*
if(password_verify($password, $phash)){

	$sql = query("UPDATE userinfo SET hash = '$hash' WHERE id = '$id'");
	$row = mysqli_fetch_row($sql);	
	echo("loginsucceed");

	}

}else{

	echo("incorrect");

}
*/
?>
