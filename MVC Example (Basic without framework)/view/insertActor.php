<!--
To change this template, choose Tools | Templates
and open the template in the editor.
-->
<!DOCTYPE html>
<head>
    <link rel="stylesheet" type="text/css" href="assignment1style.css">
</head>

<body>
<div id="banner">
    <p id="bannerText">
        <strong>Add Actor</strong>
    </p>
</div>

<form action="<?php echo $_SERVER['PHP_SELF'];?>" class="backButton">
    <input type="submit" value="Back to Actors">
</form>

<p>Add an Actor</p></br>
<form action="<?php echo $_SERVER['PHP_SELF'];?>" method="post" name="insertForm">
    <label for="firstName">First Name: </label>
    <input type="text" name="i_firstName" id="i_firstName"></br></br>
    <label for="lastName">Last Name: </label>
    <input type="text" name="i_lastName" id="i_lastName"></br></br>
    <input type="submit">
</form>

<h1><?php if(isset($lastOperationResults)){echo $lastOperationResults;}?></h1>
    </body>
</html>
