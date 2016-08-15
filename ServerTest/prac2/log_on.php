<?php
 
$id = "adepter";
$pass = 1234567;
 
$data1 = $_POST["id_data"];
$data2 = $_POST["pass_data"];
 
$loginOk = "로그인 성공";
$loginBad = "로그인 실패";
 
if($id == $data1 && $pass == $data2){
    echo '로그인 상태 : '.$loginOk;
}
else{
    echo '로그인 상태 : '.$loginBad;
}
 
?