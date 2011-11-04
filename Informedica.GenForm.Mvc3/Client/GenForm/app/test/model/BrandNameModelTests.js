Ext.define('GenForm.test.model.BrandNameModelTests', {

    describe: 'GenForm.model.product.BrandName',

    tests: function () {
        var me = this;

        me.getBrandNameModel = function () {
            var model = Ext.ModelManager.getModel('GenForm.model.common.IdName');
            
            model.setProxy({
                type: 'direct',
                directFn: GenForm.server.UnitTest.GetBrandNames
            })

            return model;
        };

        it('BrandNameModel should be defined', function () {
            expect(me.getBrandNameModel()).toBeDefined();
        });

        it('After calling load an Instance of BrandNameModel should be created', function () {
            var model;

            me.getBrandNameModel().load('', {
                callback: function (record) {
                    model = record;
                }
            })

            waitsFor(function () {
                return model ? true: false;
            }, 'fetching BrandNameModel', GenForm.test.waitingTime);
        });

        it('BrandModel data contains a BrandName', function () {
            var model;

            me.getBrandNameModel().load('', {
                callback: function (result) {
                    model = result;
                }
            });

            waitsFor(function () {
                if (model) {
                    if (model.data) {
                        return model.data['Name'] !== undefined ? true: false;
                    }
                }
                return false;
            }, 'checking if model contains Name prop', GenForm.test.waitingTime);
        });

    }
});