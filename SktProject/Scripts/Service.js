


app.service("myProductService", function ($http) {

    this.getProducts = function () {
        return $http.get("/Products/Index");
    }

    this.deleteProduct = function (product) {

        var response = $http({
            method: "POST",
            url: "/Admin/Delete/",
            params: {
                id: JSON.stringify(product)
            },
            dataType: "json"
        });
        console.log(response);
        return response;
    }



})

//Category Service
app.service("myCatService", function ($http) {

    this.getCategories = function () {
        return $http.get("/Categories/Index");
    }

    this.AddCategory = function (category) {
        var response = $http({
            method: "POST",
            url: "/Categories/Create",
            data: JSON.stringify(category),
            dataType: "json"
        })
        return response;

    }

    this.DeleteCat = function (category) {

        var response = $http({
            method: "POST",
            url: "/Categories/Delete",
            data: JSON.stringify(category),
            dataType: "json"
        })
        return response;
        
    }

})

//home service
appHome.service("myhomeService", function ($http) {

    this.clearCart = function () {
        return $http.get("/ShoppingCart/ClearCart");
    }

    this.shopCartDelete = function (cartdata) {
        var response = $http({
            method: "POST",
            url: "/ShoppingCart/RemoveToCart/",
            params: cartdata,
            dataType: "json"
        });
        console.log(response);
        return response;
    }


    this.shopCartAdd = function (product) {
        var response = $http({
            method: "POST",
            url: "/ShoppingCart/AddToCart/",
            params: product,
            dataType: "json"
        });
        console.log(response);
        return response;
    }

    this.getShopCart = function () {
        return $http.get("/ShoppingCart/Index");
    }

    this.getShopTotal = function () {
        return $http.get("/ShoppingCart/CartTotal");
    }
    

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
//appUser.service("myHomeService", function ($http) {

//    this.getDatas = function () {
//        return $http.get("/Home/IndexProduct");
//    }

//    this.getDataCats = function () {
//        return $http.get("IndexCat");
//    }



//})
