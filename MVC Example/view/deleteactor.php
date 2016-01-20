<?php

if(isset($_GET["actorid"]))
{
    require("../Business/Actor.php");

    Actor::delete($_GET["actorid"]);

    $searchTextGET = "";
    if(isset($_GET['searchText']))
    {
        $searchTextGET = "&searchText=".$_GET['searchText'];
    }
    header("Location: actors.php?pageNum=".$_GET['pageNum'].$searchTextGET);
}
else
{
    $searchTextGET = "";
    if(isset($_GET['searchText']))
    {
        $searchTextGET = "&searchText=".$_GET['searchText'];
    }
    header("Location: actors.php?pageNum=".$_GET['pageNum'].$searchTextGET);
}

?>
