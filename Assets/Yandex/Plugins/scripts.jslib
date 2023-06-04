mergeInto(LibraryManager.library, {

    ShowYandexAd: function () {
        window.ysdk.adv.showFullscreenAdv({
            callbacks: {
                onOpen: function() {
                  myGameInstance.SendMessage('Yandex','AdOpened');
                },
                onClose: function(wasShown) {
                  myGameInstance.SendMessage('Yandex','AdClosed');
                },
                onError: function(error) {
                  myGameInstance.SendMessage('Yandex','AdClosed');
                }
            }
        });
    },
    
    ShowYandexRewardedAd: function () {
        window.ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                  myGameInstance.SendMessage('Yandex','AdOpened');
                },
                onRewarded: () => {
                  myGameInstance.SendMessage('Yandex','GiveReward');
                },
                onClose: () => {
                  myGameInstance.SendMessage('Yandex','AdClosed');
                }, 
                onError: (e) => {
                  myGameInstance.SendMessage('Yandex','AdClosed');
                }
            }
        })

    },

    SaveExtern: function(progress){
        var progressString = UTF8ToString(progress);
        var myObj = JSON.parse(progressString);
        console.log(progressString);
        
        player
            .setData(myObj)
            .then(()=>{console.log('saved')})
            .catch(()=>{console.log('unsaved')});
    },

    LoadExtern: function(){
        player.getData().then(data=>{
            const myJSON = JSON.stringify(data);
            myGameInstance.SendMessage('Yandex','SetProgress', myJSON);    
        });
    },
    
    Log: function(log){
        console.log(UTF8ToString(log));
    },
});