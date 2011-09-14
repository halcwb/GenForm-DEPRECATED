Ext.define('GenForm.test.model.ProductModelTests', {

    describe: 'ProductModelShould',

    tests: function () {
        var me = this, record,
            modelName = 'GenForm.model.product.Product',
            testProduct = {
                Name: 'paracetamol 500 mg tablet',
                BrandName: 'Paracetamol',
                brands: [
                    {BrandName: 'Paracetamol'},
                    {BrandName: 'Dynatra'}
                ],
                GenericName: 'paracetamol',
                ShapeName: 'tablet',
                PackageName: 'tablet',
                Quantity: '500',
                Unit: 'mg'
            };

        beforeEach(function () {
           if (!record) record = Ext.create(modelName);
        });

        me.createTestRecord = function () {
            return Ext.ModelManager.create(testProduct, modelName);
        };

        me.getModel = function () {
            var model = Ext.ModelManager.getModel(modelName);
            model.setProxy(me.getTestProxy());
            return model;
        };

        me.setUpTestProxy = function () {
            record.setProxy(me.getTestProxy());
        };

        me.getTestProxy = function () {
            return Ext.create('Ext.data.proxy.Direct', {
                type: 'direct',
                api: {
                    read: Tests.GetProduct,
                    save: Tests.SaveProduct
                }
            });
        };


        it('be defined', function () {
            expect(record).toBeDefined();
        });

        it('be created using ModelManager.create', function () {
            expect(me.createTestRecord()).toBeDefined();
        });

        it('have a ProductName', function () {
            expect(record.data.Name).toBeDefined();
        });

        it('have a GenericName', function () {
            expect(record.data.GenericName).toBeDefined();
        });

        it('have GenericName is paracetamol', function () {
           expect(me.createTestRecord().data.GenericName).toBe('paracetamol');
        });

        it('have a list of possible GenericNames', function () {
            expect(record.generics).toBeDefined();
        });

        it('have a BrandName', function () {
            expect(record.data.BrandName).toBeDefined();
        });

        it('have a list of brands', function () {
            expect(record.brands).toBeDefined();
        });

        it('have a ShapeName', function () {
            expect(record.data.ShapeName).toBeDefined();
        });

        it('have a list of shapes', function () {
            expect(record.shapes).toBeDefined();
        });

        it('have a PackageName', function () {
            expect(record.data.PackageName).toBeDefined();
        });

        it('have a list of packages', function () {
            expect(record.packages).toBeDefined();
        });

        it('have a Quantity', function () {
            expect(record.data.Quantity).toBeDefined();
        });

        it('have substances', function () {
            expect(record.substances).toBeDefined();
        });

        it('should have routes', function () {
            expect(record.routes).toBeDefined();
        });

        it('get a success message upon saving the productmodel', function () {
            var result;
            Product.SaveProduct(record.data, function (record) {
                result = record
             });

            waitsFor(function () {
               return result ? true: false;
            }, 'waiting for callback of Product.Save', GenForm.test.waitingTime);
        });

        it('post a valid ProductModel', function () {
            var callData;
            spyOn(Product.SaveProduct.directCfg.method, 'getCallData').andCallFake(
                    function () {
                        callData = arguments;
                    });

            try {
                Product.SaveProduct(record.data, function () {
                });
            } catch(e) {
                console.log(e);
            }

            expect(callData).toBeDefined();
        });

        it ('be able to load a record using the model', function () {
            var result;
            me.getModel().load('2', {
                callback: function (record) {
                    result = record;
                }
            });

            waitsFor(function () {
                return result ? true: false;
            }, 'product with id = 1', GenForm.test.waitingTime);

        })
    }
});