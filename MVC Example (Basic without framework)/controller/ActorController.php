<?php

require_once('../model/ActorModel.php');

class ActorController
{
    public $model;
    
    public function __construct()
    {
        $this->model = new ActorModel();
    }
    
    public function displayAction($pageNum,$searchText)//displays page with actors
    {
        $firstRecordNum = ($pageNum-1)*25;
        if(!empty($searchText))
        {
            $arrayOfActors = $this->model->getActorsBySearch($firstRecordNum,25,$searchText);
        }
        else
        {
            $arrayOfActors = $this->model->getActors($firstRecordNum,25);
        }
        include '../view/displayActors.php';
    }

    public function insertAction()
    {
        include '../view/insertActor.php';
    }

    public function updateAction($actorID)
    {
        $currentActor = $this->model->getActor($actorID);

        include '../view/editactor.php';
    }

    public function deleteAction($actorID)//includes commit
    {
        $this->model->deleteActor($actorID);

        include '../view/displayActors.php';
    }

    public function commitInsertAction($fName,$lName)
    {
        $lastOperationResults = "";

        $newActor = $this->model->makeInsertableActor($fName,$lName);

        $lastOperationResults = $this->model->insertActor($newActor);

        include '../view/insertActor.php';
    }


    public function commitUpdateAction($actorID,$fName,$lName)
    {
        $lastOperationResults = "";

        $currentActor = $this->model->getActor($actorID);

        $currentActor->setFirstName($fName);
        $currentActor->setLastName($lName);

        $lastOperationResults = $this->model->updateActor($currentActor);

        include '../view/editactor.php';
    }


}

?>
