/**
 * Created by .
 * User: halcwb
 * Date: 5/15/11
 * Time: 10:00 AM
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.controller.product.Product', function () {
    var getProductController, testController;

    getProductController = function () {
        if (!testController) {
            testController = Ext.create('GenForm.controller.product.Product', {
                id: 'testProductController',
                application: GenForm.application
            });
        }
        return testController;
    };

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
        record.data.ProductName = 'paracetamol 500 mg';
        record.data.GenericName = 'paracetamol';
        record.data.BrandName = 'Paracetamol';
        record.data.ShapeName = 'tablet';
        record.data.Quantity = '1';
        record.data.Unit = 'stuk';
        record.data.PackageName = 'tablet';

        form.loadRecord(record);

        spyOn(controller, 'onProductSaved');
        controller.saveProduct(form.up('panel').down('toolbar').down('button'));

        waitsFor(function () {
            return controller.onProductSaved.wasCalled;
        }, 'waiting for onProductSaved call', 1000);
    });

});