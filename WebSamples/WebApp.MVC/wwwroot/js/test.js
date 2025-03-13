//console.log('test.js loaded');
//console.log('Hello world');
//let x = 10;
//let y = 20;

//if (x < y) {
//    console.log('x is less than y');
//}

//else if (x === y) {
//    console.log('x is equal y');
//}

//else {
//    console.log('x is greater than y');
//}

//let userInput = prompt('Enter odd value from 1 to 5');

//switch (userInput) {
//    case '1': //if (userInput === '1')
//        console.log('Your input is 1');
//        break;
//    case '3':
//        console.log('Your input is 3');
//        break;

//    case '2':
//    case '4':
//        console.log('Your input is even');
//        break;

//    case '5':
//        console.log('Your input is 5');
//        break;
//    default:
//        console.log('Your input is incorrect');
//        break;
//}

let array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

//for (let i = 0; i < array.length; i++ ) {
//    console.log(array[i]);
//}

//let sum = 0;
//let i = 0;
//while (sum < 20) {

//    if (i >= array.length) {
//        break; //stop loop execution (as well as return)
//    }

//    if (array[i] === 5) {
//        i++;
//        continue; //skip iteration
//    }

//    sum += array[i];
//    i++;
//}

//i = 1;

//do {
//    console.log(array[array.length - i]);
//    i++;

//}
//while (i <= 2)


//let array2 = [1, '1', 2];

//console.log(array2);

//console.log('array[2]', array2[2]);

//array2['AAA'] = 15;
//array2.C = 14;

//console.log(array2);
//console.log('array[2]', array2[1]);

//equivalent of foreach in C#
for (let item of array) {
    console.log(item);
}

let obj = { A: 15, B: "hello world", C: true };

for (let item in obj) {
    console.log(item);
}

