<?
  $sno = $_POST["sno"];
  $sname = $_POST["sname"];
  $sex = $_POST["sex"];
  $major = $_POST["major"];
  $syear = $_POST["syear"];
  $avr = $_POST["avr"];

   require('connect_student_db.php');
   $sql="insert into student(sno,sname,sex,major,syear,svr)
   values('$sno','$sname','$sex','$major','$syear','$avr')";
   $result=mysql_query($sql,$connect_student_db);
   mysql_close($connect_student_db);
   mysql_close($connect_student_db);
   if($result)
   	echo ("query complete");
   else
   	 echo("query fail");
   	?>