Ext.define('GenForm.test.server.ProductTests', {
    describe: 'ProductsShould',

    tests: function () {
        //noinspection JSUnusedGlobalSymbols
        var testProduct = {
            Id: 0,
            ProductName: 'dopamine Dynatra infusievloeistof 200 mg 5 mL ampul',
            DisplayName: 'dopamine Dynatra infusievloeistof 200 mg 5 mL ampul',
            BrandName: 'Dynatra',
            GenericName: 'dopamine',
            ShapeName: 'infusievloeistof',
            PackageName: 'ampul',
            Quantity: 5,
            UnitName: 'mL',
            Substances: [
                {Id: 0, SortOrder: 1, Substance: 'dopamine', Quantity: 200, Unit: 'mg'},
                {Id: 0, SortOrder: 2, Substance: 'water', Quantity: 5, Unit: 'mL'}
            ],
            Routes: [
                {Id: 0, Route: 'iv'}
            ]
        };
    

        it('be defined', function () {
            expect(Product.GetProduct).toBeDefined();
        });

        it('return a  success value when a method is called', function () {
            var result;

            Tests.GetProduct(1, function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of sussess value');

            runs(function () {
                expect(result.success).toBeTruthy();
            });
        });

        it('return a valid product when GetProduct is called', function () {
            var result;

            Tests.GetProduct(1, function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of GetProduct');

            runs(function () {
                expect(result.data).toBeDefined();
                expect(result.data.ProductName).toBe("dopamine Dynatra 5 mL ampul");
            });

        });

        it('save a fully populated product', function () {
            var result;

            Tests.SaveProduct(testProduct, function (response) {
                result = response;
                if (!result.success) console.log(result.message);
            });

            waitsFor(function () {
                return result;
            }, 'return of SaveProduct');

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });

        it('return a saved product with id > 0', function () {
            var result;

            Tests.SaveProduct(testProduct, function (response) {
                result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of saved product with id');

            runs(function () {
                expect(result.data.Id > 0).toBeTruthy();
            });
        });

        it('not save an invalid product', function () {
            var result;
            
            testProduct.GenericName = "";
            Tests.SaveProduct(testProduct, function (response) {
                result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of save of invalid product');

            runs(function () {
                expect(result.success === false).toBeTruthy();
            });
        });

        it('delete the saved product', function () {
            var result;

            testProduct.Id = 1;
            Tests.DeleteProduct(testProduct.Id, function (response) {
                result = response;
            });
            
            waitsFor(function () {
                return result;
            });

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });
    }
});