Ext.define('GenForm.test.server.ProductTests', {
    describe: 'ProductsShould',

    tests: function () {
        //noinspection JSUnusedGlobalSymbols
        var testProduct = {
            Id: 0,
            ProductName: 'dopamine Dynatra infusievloeistof 200 mg 5 mL ampul',
            DisplayName: 'dopamine Dynatra infusievloeistof 200 mg 5 mL ampul',
            Brand: 'Dynatra',
            Generic: 'dopamine',
            Shape: 'infusievloeistof',
            Package: 'ampul',
            Quantity: 5,
            Unit: 'mL',
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
            });

            waitsFor(function () {
                return result;
            }, 'return of SaveProduct');

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });
    }
});