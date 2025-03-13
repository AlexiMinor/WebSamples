//function outerFunc() {
//    const outerVar = "I'm the outer variable";
//    function innerFunc() {
//        console.log(outerVar);
//    }
//    return innerFunc;
//}

//const myFunc = outerFunc();

//myFunc();

function makeSumCalculator(x) {
    return function (y) {
      return x + y;
    }
}

let add5 = makeSumCalculator(5);

// makeSumCalculator(5){
// function(y){
//return 5+y
//}
//}

let add10 = makeSumCalculator(10);

console.log(add5(2));
console.log(add10(2));