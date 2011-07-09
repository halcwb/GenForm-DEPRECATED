Ext.define('GenForm.test.model.ProductModelTests', {

    describe: 'GenForm.model.product.Product',

    tests: function () {
        var getProductModel, loadProductModel,  productModel, createProductModel, createProxy;

        getProductModel = function () {
            return Ext.ModelManager.getModel('GenForm.model.product.Product');
        };

        createProductModel = function () {
            var testProduct = {
                ProductName: 'paracetamol 500 mg tablet',
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

            return Ext.ModelManager.create(testProduct, 'GenForm.model.product.Product');
        };

        createProxy = function () {
            return Ext.create('Ext.data.proxy.Direct', {
                type: 'direct',
                api: {
                    save: Product.SaveProduct
                }
            });
        };

        loadProductModel = function () {
            productModel = null;
            getProductModel().load('1', {
                callback: function(result) {
                    productModel = result;
                }
            });
        };

        it('ProductModel should be defined', function () {
            expect(getProductModel()).toBeDefined();
        });

        it('can be created using ModelManager.create', function () {
            expect(createProductModel()).toBeDefined();
        })

        it('Should have a ProductName', function () {
            expect(createProductModel().data.ProductName).toBeDefined();
        });

        it('Should have a GenericName', function () {
            expect(createProductModel().data.GenericName).toBeDefined();
        });

        it('GenericName should be paracetamol', function () {
           expect(createProductModel().data.GenericName).toBe('paracetamol');
        });

        it('Should have a list of possible GenericNames', function () {
            expect(createProductModel().generics).toBeDefined();
        });

        it('Should have a BrandName', function () {
            expect(createProductModel().data.BrandName).toBeDefined();
        });

        it('Should have a list of brands', function () {
            expect(createProductModel().brands).toBeDefined();
        });

        it('Should have a ShapeName', function () {
            expect(createProductModel().data.ShapeName).toBeDefined();
        });

        it('Should have a list of shapes', function () {
            expect(createProductModel().shapes).toBeDefined();
        });

        it('Should have a PackageName', function () {
            expect(createProductModel().data.PackageName).toBeDefined();
        });

        it('Should have a list of packages', function () {
            expect(createProductModel().packages).toBeDefined();
        });

        it('Should have a Quantity', function () {
            expect(createProductModel().data.Quantity).toBeDefined();
        });

        it('should have substances', function () {
            expect(createProductModel().substances).toBeDefined();
        });

        it('Product should have routes', function () {
            expect(createProductModel().routes).toBeDefined();
        });

        it('createProxy should return a proxy', function () {
            expect(createProxy().$className).toBe('Ext.data.proxy.Direct');
        });

    /*
        Does not work yet???
        it('should if a product is save/failure message is returned', function () {
            var proxy, model, result = null;
            model = createProductModel();
            proxy = createProxy();
            model.setProxy(proxy);
            console.log(model.getProxy());

            createProductModel().save({
                scope: this,
                callback: function (record, operation) {
                    result = record
                }
            });

            waitsFor(function () {
                return result ? true : fals;
            }, "waiting for result of save to return")
        });
    */
        it('should get a success message upon saving the productmodel', function () {
            var model = createProductModel(), result;
            Product.SaveProduct(model.data, function (record) {
                result = record
             });

            waitsFor(function () {
               return result ? true: false;
            }, 'waiting for callback of Product.Save', 1000);
        });

        it('should post a valid ProductModel', function () {
            var model = createProductModel(), callData;
            spyOn(Product.SaveProduct.directCfg.method, 'getCallData').andCallFake(
                    function () {
                        callData = arguments;
                    });

            try {
                Product.SaveProduct(model.data, function (record) {
                });
            } catch(e) {
                console.log(e);
            }

            expect(callData).toBeDefined();
        });

        it ('loading a productmodel from the server with id = 1', function () {
            var result;
            getProductModel().load('2', {
                callback: function (record) {
                    result = record;
                }
            });

            waitsFor(function () {
                return result ? true: false;
            }, 'waiting for product with id = 1', 1000);

        })
    }
});