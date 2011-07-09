Ext.define('GenForm.test.model.BrandNameModelTests', {

    describe: 'GenForm.model.product.BrandName',

    tests: function () {
        var getBrandNameModel,
            waitingTime = 100;

        getBrandNameModel = function () {
            var model = Ext.ModelManager.getModel('GenForm.model.product.BrandName');
            model.setProxy({
                type: 'direct',
                directFn: Product.GetBrandNames
            })

            return model;
        };

        it('BrandNameModel should be defined', function () {
            expect(getBrandNameModel()).toBeDefined();
        });

        it('After calling load an Instance of BrandNameModel should be created', function () {
            var model;

            getBrandNameModel().load('', {
                callback: function (record) {
                    model = record;
                }
            })

            waitsFor(function () {
                return model ? true: false;
            }, 'fetching BrandNameModel', waitingTime);
        });

        it('BrandModel data contains a BrandName', function () {
            var model;

            getBrandNameModel().load('', {
                callback: function (result) {
                    model = result;
                }
            });

            waitsFor(function () {
                if (model) {
                    if (model.data) {
                        return model.data['BrandName'] !== undefined ? true: false;
                    }
                }
                return false;
            }, 'checking if model contains BrandName prop', waitingTime);
        });

    }
});