<?php

    $name = "denti";
    $level = 1;
    $speed = 12.3;
    $hp = 50;
    $damage = 10;
    
    //hp 감소
    $hp = $hp - $damage;
    
    echo 'name : '.$name.', ';
    echo 'level : '.$level.', ';
    echo 'speed : '.$speed.', ';
    echo 'damage : '.$damage.', ';
    echo 'hp : '.$hp;

?>