Ext.define('GenForm.test.server.ProductTests', {
    describe: 'ProductsShould',

    tests: function () {
        //noinspection JSUnusedGlobalSymbols
        var me = this, guidGenerator = Ext.create('GenForm.test.util.GuidGenerator'),
            testProduct = {
                Id: guidGenerator.emptyGuid(),
                Name: 'dopamine (Dynatra) 200 mg in 5 mL infusievloeistof per ampul',
                DisplayName: 'dopamine Dynatra infusievloeistof 200 mg 5 mL ampul',
                BrandName: 'Dynatra',
                GenericName: 'dopamine',
                ShapeName: 'infusievloeistof',
                PackageName: 'ampul',
                Quantity: 5,
                UnitName: 'mL',
                Substances: [
                    {Id: guidGenerator.emptyGuid(), SortOrder: 1, Substance: 'dopamine', Quantity: 200, Unit: 'mg'},
                    {Id: guidGenerator.emptyGuid(), SortOrder: 2, Substance: 'water', Quantity: 5, Unit: 'mL'}
                ],
                Routes: [
                    {Id: guidGenerator.emptyGuid(), Route: 'iv'}
                ]
            };
    

        it('be defined', function () {
            expect(Product.GetProduct).toBeDefined();
        });

        it('return a  success value when a method is called', function () {
            var result;

            Tests.GetProduct(guidGenerator.emptyGuid(), function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of success value', GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeTruthy();
            });
        });

        it('return a valid product when GetProduct is called', function () {
            var result;

            Tests.GetProduct(guidGenerator.createGuid(), function (response) {
                  result = response;
            });

            waitsFor(function () {
                return result;
            }, 'return of GetProduct', GenForm.test.waitingTime);

            runs(function () {
                expect(result.data).toBeDefined();
                expect(result.data.Name).toBe(testProduct.Name);
            });

        }); 

        it('save a fully populated product', function () {
            var result;
            testProduct.Id = guidGenerator.createGuid();
            Tests.SaveProduct(testProduct, function (response) {
                result = response;
                if (!result.success) console.log(result);
            });

            waitsFor(function () {
                return result;
            }, 'return of SaveProduct', GenForm.test.waitingTime);

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
            }, 'return of saved product with id', GenForm.test.waitingTime);

            runs(function () {
                expect(result.data.Id.length > 0).toBeTruthy();
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
            }, 'return of save of invalid product',GenForm.test.waitingTime);

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
            }, 'Delete saved Product',GenForm.test.waitingTime);

            runs(function () {
                expect(result.success).toBeTruthy();
            });

        });
    }
});