//drop down navbar
$(".handel").on('click', function(){
    $("ul").toggleClass("stretch");
    
});

//If user clicks anywhere outside of the ul, Modal will close
$(document).click(function(e){
    if (".stretch") {
        if(!$(e.target).parents().andSelf().is('ul')){
         $("ul").removeClass("stretch"); }             
  }
});

//open login
$(".login").on('click', function(){
    $('#modal-wrapper').css({display: "block"});
    $('.modal-content2').css({display: "none"});
    $('.modal-content1').css({display: "block"});
    $('.animate').css({animation: "zoom1 0.4s"}).unbind('animationend');
    $("ul").removeClass("stretch");
});

//open Signup
$(".signup").on('click', function(){
    $('#modal-wrapper').css({display: "block"});
    $('.modal-content1').css({display: "none"});
    $('.modal-content2').css({display: "block"});
    $('.animate').css({animation: "zoom1 0.4s"}).unbind('animationend');
    $("ul").removeClass("stretch");
});


//close login
$(".close").on('click', function(){
    $('.animate').css({animation: "zoom2 0.2s"}).on('animationend ', HideTheElementAfterAnimation);
});

function HideTheElementAfterAnimation(){
    $("#modal-wrapper").css("display", "none");
}

//If user clicks anywhere outside of the modal, Modal will close
$(document).click(function(e){
    if (!$(e.target).is(".unique")) {
        if (!$(e.target).parents().andSelf().is('.animate')){
            $('.animate').css({animation: "zoom2 0.2s"}).on('animationend ', HideTheElementAfterAnimation);
        }      
    }
});

//switch to login
$(".switchToLogin").on('click', function(){
    $('.modal-content2').css({display: "none"});
    $('.modal-content1').css({display: "block"});
    $('.animate').css({animation: "none"}).unbind('animationend');
});

//switch to singup
$(".switchToSignup").on('click', function(){
    $('.modal-content2').css({display: "block"});
    $('.modal-content1').css({display: "none"});
    $('.animate').css({animation: "none"}).unbind('animationend');
});




