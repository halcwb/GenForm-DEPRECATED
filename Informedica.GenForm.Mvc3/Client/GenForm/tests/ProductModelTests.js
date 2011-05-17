/**
 * Created by .
 * User: hal
 * Date: 7-5-11
 * Time: 9:42
 * To change this template use File | Settings | File Templates.
 */
describe('GenForm.model.product.ProductModel', function () {
    var getProductModel, loadProductModel,  productModel, createProductModel;

    getProductModel = function () {
        return Ext.ModelManager.getModel('GenForm.model.product.ProductModel');
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

        return Ext.ModelManager.create(testProduct, 'GenForm.model.product.ProductModel');
    };

    loadProductModel = function () {
        productModel = null;
        getProductModel().load('0', {
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

});