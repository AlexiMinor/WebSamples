//let messageFunc = function () {
//    console.log('Hello world');
//}

let name = 'Bob';

writeMessage();

let greetUserFunc = greetUser(name, getGreetingMessage);

greetUserFunc();
//calculateSum(2,3,15);

//messageFunc();

//console.log(array);


//let array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

//function calculateSum(x, y) {
//    console.log(arguments);
//    //if (x === undefined) {
//    //    x = 0;
//    //}
//    console.log(`${x}+${y}=${x + y}`);
//}

function writeMessage() {
    let name = 'Alice';
    let resultMessage = getGreetingMessage(name);
    console.log(resultMessage);
    //return;
}


function getGreetingMessage(name) {
    let greetingMessage = `Hello, ${name}`;
    return greetingMessage;
}

function greetUser(name, greetMessageGenerationFunc) {
    let result = greetMessageGenerationFunc(name);

    return function () {
        console.log(result);
    }
}


//let arrowFunc = (p1, p2, p3) => expression;
//let arrowFunc = function (p1, p2, p3) {
//    return expression;
//}


let sum = (a, b) => {
    let result = a + b;
    return result;
}

console.log(sum(2, 2));

let isInAge = confirm('Do you have 18?');

let welcomeMessageResult = (isInAge) ?
    () => console.log('Hello there') :
    () => console.log('You are not allowed');

welcomeMessageResult();