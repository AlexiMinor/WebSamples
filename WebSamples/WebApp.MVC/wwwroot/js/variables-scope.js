//let data = 0;

{
    let data = 1;
    /*const*/ constData = 2;
    console.log(data);
    //console.log(constData);
}

//console.log(data);
//console.log(constData);

doSmth();

function doSmth () {
    console.log('a');
    function doSmthElse() {
        console.log('b');
    }

    doSmthElse();
}

doSmthElse();