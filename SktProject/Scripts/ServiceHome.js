appHome.service("myhomeService", function ($http) {

    this.gethomeCategories = function () {
        return $http.get("/Home/IndexCat");
    }

    this.getDatas = function () {
        return $http.get("/Home/IndexProduct");
    }

    this.selectCat = function (cat) {

        var response = $http({
            method: "POST",
            url: "/Home/GetCatProduct/",
            params: cat,
            dataType: "json"
        });
        console.log(response);
        return response;
    }

});