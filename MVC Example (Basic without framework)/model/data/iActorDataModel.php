<?php

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
interface iActorDataModel
{
    public function connectToDB();

    public function closeDB();

    public function selectActors($start,$count);
    
    public function searchActors($start,$count,$searchText);

    public function selectActorById($actorId);

    public function fetchActors();

    public function insertActor($first_name,$last_name);

    public function updateActor($actorId,$first_name,$last_name);

    public function deleteActor($ActorToDelete);

    // field access functions
    public function fetchActorID($row);

    public function fetchActorFirstName($row);

    public function fetchActorLastName($row);

}
?>
