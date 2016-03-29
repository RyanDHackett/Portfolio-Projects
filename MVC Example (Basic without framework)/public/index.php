<?php

require_once '../controller/ActorController.php';

$actorController = new ActorController();
//
//if(isset($_GET['idUpdate']))
//{
//    $actorController->updateAction($_GET['idUpdate']);
//}

if(!empty($_POST["searchText"]))//search form submitted
{
    $actorController->displayAction(1,$_POST["searchText"]);
}
elseif(!empty($_GET["pageNum"]))//Next or previous pressed
{
    $actorController->displayAction($_GET["pageNum"],$_GET["searchText"]);
}
elseif (isset($_GET['action']))
{
    if($_GET['action'] == "add")
        $actorController->insertAction();
    elseif($_GET['action'] == "edit")
        $actorController->updateAction($_GET["id"]);
    elseif($_GET['action'] == "delete")
        $actorController->deleteAction($_GET["id"]);//a delete action has been requested
}
elseif(isset($_POST["i_firstName"]))//an insert action has been requested
{
    $actorController->commitInsertAction($_POST["i_firstName"],$_POST["i_lastName"]);
}
elseif(isset($_POST["u_firstName"]))//an update action has been requested
{
    $actorController->commitUpdateAction($_POST["idToUpdate"],$_POST["u_firstName"],$_POST["u_lastName"]);
}
else
{
    $pageNum = 1;
    $actorController->displayAction($pageNum,"");
}


?>

