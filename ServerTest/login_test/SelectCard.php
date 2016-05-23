<?
    //$ID = $_REQUEST["ID"];
    $conn = mysql_connect("localhost","root","111111") or die("접속에 실패했습니다.");
    mysql_select_db("test_login");
    $query = mysql_query("SELECT * FROM skill_card");

    if(!$query)
    	id("DB access failed : " .mysql_error());

    $num_result = mysql_num_rows($query);

    header ("Contect-type: text/xml");
    echo"<xml version=\"1.0\" encoding=\"utf-8\" ?> \n";

    echo "<record>";
    for($i=0; $i<$num_result; ++$i)
    {
      $row = mysql_fetch_array($query);

      echo "<item> ";
      echo "SKILLNAME='$row[SKILLNAME]' ";
      echo "COST='$row[COST]' ";
      echo "TYPE='$row[TYPE]' ";
      echo "DAMAGE='$row[DAMAGE]' ";
      echo "RANGEVIEW='$row[RANGEVIEW]' ";
      echo "RANGEOFACT='$row[RANGEOFACT]' ";
      echo ."</item>";
    }
    echo"</record>";
    mysql_close($conn);
?>