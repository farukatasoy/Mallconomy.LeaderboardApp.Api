- Proje swagger arayuzune sahiptir, ilgili testleri bu panel uzerinden gerceklestirebilirsiniz.

ENDPOINTLER VE ACIKLAMALARI

1-) [POST] api/Leaderboard
  Leaderboard'i olusturdugumuz endpointtir. Date degeri alir. Tarihte gecen yil ve aya gore, o ayin liderlik tablosunu olusturur. O tarihe ait olusturulmus bir leaderboard varsa olusturmaz. Olusturmayi yaparken approved false olanlari dahil etmez. Date degeri ornegi : "2023-02"
  
2-) [GET] api/Leaderboard/{date}
  Aldigi date degerinin yil ve ay parametlerine bakarak, o tarihlerde olusturulmus leaderboard'i verir. User id, rank, total points parametrelerin cikti da gozlemlenmektedir. Date degeri ornegi : "2023-02"

3-) [GET] api/Leaderboard/{date},{userId}
    Aldigi date degerinin yil ve ay parametlerine bakarak, o tarihlerde olusturulmus leaderboard'ta, aldigi kullanici id ye gore, o kullanicinin bilgilerini verir. User id, rank, total points parametrelerin cikti da gozlemlenmektedir. Date degeri ornegi : "2023-02", userName degeri ornegi : "628b514c1ba3abddabf15feb"

2-) [GET] api/Leaderboard/{userId}
  Aldigi userId parametresine gore mevzubahis kullanicinin, aldigi odul ya da odulleri, ay ay listeler. userName degeri ornegi : "628b514c1ba3abddabf15feb"
