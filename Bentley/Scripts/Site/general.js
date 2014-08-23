
function image()  {

    var number = 0;

    // imageArray
    image[number++] = "<img src='/images/BackgroundImages/5.jpg'/>"
    image[number++] = "<img src='/images/BackgroundImages/7.jpg'/>"
    image[number++] = "<img src='/images/BackgroundImages/8.jpg'/>"
    image[number++] = "<img src='/images/BackgroundImages/15.jpg'/>"

    var increment = Math.floor(Math.random() * number);
    return increment;
}
