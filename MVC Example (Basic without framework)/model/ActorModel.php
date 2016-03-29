<?php

require_once '../model/Actor.php';


//require_once '../model/data/MySQLiActorDataModel.php';
require_once '../model/data/PDOMySQLActorDataModel.php';

class ActorModel
{
    private $m_DataAccess;
    
    public function __construct()
    {
        //$this->m_DataAccess = new MySQLiActorDataModel();
        $this->m_DataAccess = new PDOMySQLActorDataModel();
    }
    
    public function __destruct()
    {
        // not doing anything at the moment
    }
            
    public function getActors($start,$count)
    {
        $this->m_DataAccess->connectToDB();
        
        $arrayOfActorObjects = array();
        
        $this->m_DataAccess->selectActors($start,$count);

        $i = 0;
        while($row =  $this->m_DataAccess->fetchActors())
        {
            $currentActor = new Actor($this->m_DataAccess->fetchActorID($row),
                    $this->m_DataAccess->fetchActorFirstName($row),
                    $this->m_DataAccess->fetchActorLastName($row));
            
            $arrayOfActorObjects[$i] = $currentActor;
            $i++;
        }
        
        $this->m_DataAccess->closeDB();
        
        return $arrayOfActorObjects;
    }

    public function getActorsBySearch($start,$count,$searchText)
    {
        $this->m_DataAccess->connectToDB();

        $arrayOfActorObjects = array();

        $this->m_DataAccess->searchActors($start,$count,$searchText);

        $i = 0;
        while($row =  $this->m_DataAccess->fetchActors())
        {
            $currentActor = new Actor($this->m_DataAccess->fetchActorID($row),
                $this->m_DataAccess->fetchActorFirstName($row),
                $this->m_DataAccess->fetchActorLastName($row));

            $arrayOfActorObjects[$i] = $currentActor;
            $i++;
        }

        $this->m_DataAccess->closeDB();

        return $arrayOfActorObjects;
    }
    
    public function getActor($actorID)
    {
        $this->m_DataAccess->connectToDB();
        
        $this->m_DataAccess->selectActorById($actorID);
        
        $record =  $this->m_DataAccess->fetchActors();

         $fetchedActor = new Actor($this->m_DataAccess->fetchActorID($record),
                 $this->m_DataAccess->fetchActorFirstName($record),
                 $this->m_DataAccess->fetchActorLastName($record));
            
            
        
        $this->m_DataAccess->closeDB();
        
        return $fetchedActor;
    }
    
     public function updateActor($ActorToUpdate)
    {
        $this->m_DataAccess->connectToDB();
        
        $recordsAffected = $this->m_DataAccess->updateActor($ActorToUpdate->getID(),
                $ActorToUpdate->getFirstName(),
                $ActorToUpdate->getLastName());

        $this->m_DataAccess->closeDB();
        
        return "$recordsAffected record(s) updated succesfully!";
    }

    public function insertActor($ActorToInsert)
    {
        $this->m_DataAccess->connectToDB();

        $recordsAffected = $this->m_DataAccess->insertActor($ActorToInsert->getFirstName(),$ActorToInsert->getLastName());

        $this->m_DataAccess->closeDB();

        return "$recordsAffected record(s) inserted succesfully!";
    }

    public function deleteActor($actorID)
    {
        $this->m_DataAccess->connectToDB();

        $this->m_DataAccess->deleteActor($actorID);

        $this->m_DataAccess->closeDB();

    }

    public function makeInsertableActor($first_name,$last_name)
    {
        return new Actor(-1,$first_name,$last_name);
    }

}

?>
