<!DOCTYPE html>
<head>
    <link rel="stylesheet" type="text/css" href="assignment1style.css">
</head>

<body>
<div id="banner">
    <p id="bannerText">
        <strong>Edit Actors</strong>
    </p>
</div>

<form action="<?php echo $_SERVER['PHP_SELF'];?>" class="backButton" method="get">
    <input type="submit" value="Back to Actors">
</form>
        <p>Actor you chose to update. Submit this form to finalize update</p></br>
        <form action="<?php echo $_SERVER['PHP_SELF'];?>" method="post" name="updateForm">
            <label for="firstName">First Name: </label>
            <input type="text" name="u_firstName" id="u_firstName" value="<?php echo $currentActor->getFirstName()?>"></br></br>
            <label for="lastName">Last Name: </label>
            <input type="text" name="u_lastName" id="u_lastName" value="<?php echo $currentActor->getLastName()?>"></br></br>
            <input type="hidden" name="idToUpdate" value="<?php echo $currentActor->getID()?>">
            <input type="submit">
        </form>

<h1><?php if(isset($lastOperationResults)){echo $lastOperationResults;}?></h1>

</body>
</html>
