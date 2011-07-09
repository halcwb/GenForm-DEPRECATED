Ext.define('GenForm.test.controller.ProductControllerTests', {
    describe: 'GenForm.controller.product.Product',

    tests: function () {
        var getProductController, testController, copyObject,
            waitingTime = 200;
            testProduct = {
                ProductName: '',
                ProductCode: '',
                GenericName: '',
                BrandName: '',
                ShapeName: '',
                Quantity: '',
                Unit: '',
                PackageName: ''
            };

        copyObject = function (model, data) {
            var prop;
            if (!model.data) return;

            for (prop in model.data) {
                if (data[prop]) prop = data[prop];
            }
        }

        getProductController = function () {
            if (!testController) {
                testController = Ext.create('GenForm.controller.product.Product', {
                    id: 'testProductController',
                    application: GenForm.application
                });
            }
            return testController;
        }

        it('can be created', function () {
            expect(getProductController()).toBeDefined();
        });

        it('should have a getGenFormModelProductProductModel', function () {
            expect(getProductController().getProductProductModel).toBeDefined();
        });

        it('can return a GenFormModelProductProductModel', function () {
            expect(getProductController().getProductProductModel().$className).toBe("GenForm.model.product.Product");
        });

        it('getProductProductModel should return a constructor, i.e. a function', function () {
            expect(typeof getProductController().getProductProductModel()).toBe('function');
        });

        it('can create and empty instance of a Product', function () {
           expect(getProductController().createEmptyProduct()).toBeDefined();
        });

        it('should have a getProductProductWindow function', function () {
            expect(getProductController().getProductProductWindowView).toBeDefined();
        });

        it('should have a getProductProductFormView', function () {
            expect(getProductController().getProductProductFormView).toBeDefined();
        });

        it('can fire up a productwindow', function () {
            var windowCount = Ext.ComponentQuery.query('window').length;
            getProductController().showProductWindow();

            expect(Ext.ComponentQuery.query('window').length === (windowCount + 1)).toBeTruthy();
        });

        it('should have a LoadEmptyProduct function to load the productform with an empty product', function () {
            var controller = getProductController();
            expect(controller.loadEmptyProduct).toBeDefined('loadEmptyProduct was not defined');

            spyOn(controller, 'createEmptyProduct').andCallThrough();
            controller.loadEmptyProduct(controller.getProductWindow());

            expect(controller.createEmptyProduct).toHaveBeenCalled();
            expect(controller.getProductWindow().getProductForm().getForm().getRecord()).toBeDefined('Product form should have a record');
        });

        it('when a form is created with an empty product, productname should be empty', function () {
            var window = getProductController().getProductWindow();
            expect(window.getProductForm().getProduct().data.ProductName === '').toBeTruthy();
        });

        it('should have a saveProduct function', function () {
            expect(getProductController().saveProduct).toBeDefined();
        });


        it('should save a product', function () {
            var form = getProductController().getProductWindow().getProductForm(),
                record = form.getRecord(), controller = getProductController();

            testProduct.ProductName = 'paracetamol 500 mg';
            testProduct.GenericName = 'paracetamol';
            testProduct.BrandName = 'Paracetamol';
            testProduct.ShapeName = 'tablet';
            testProduct.Quantity = '1';
            testProduct.Unit = 'stuk';
            testProduct.PackageName = 'tablet';
            copyObject(record, testProduct);

            form.loadRecord(record);

            spyOn(controller, 'onProductSaved');
            controller.saveProduct(form.up('panel').down('toolbar').down('button'));

            waitsFor(function () {
                return controller.onProductSaved.wasCalled;
            }, 'waiting for onProductSaved call', 1000);
        });


        it('should have a saveGenericName function', function () {
           expect(getProductController().saveGeneric).toBeDefined();
        });

        it('saveGeneric should be able to save a valid Generic', function () {
            var controller = getProductController(),
                form = controller.getGenericWindow().getGenericForm(),
                model = form.getRecord(),
                validGeneric = {
                    GenericName: 'paracetamol'
                };
            copyObject(model, validGeneric);

            form.loadRecord(model);
    /*
            spyOn(controller, 'onGenericSaved');
            controller.saveGeneric(form.up('panel').down('toolbar').down('button'));

            waitsFor(function () {
                return controller.onGenericSaved.wasCalled;
            }, 'waiting for onGenericSaved call', waitingTime)
    */

        });

    }
});