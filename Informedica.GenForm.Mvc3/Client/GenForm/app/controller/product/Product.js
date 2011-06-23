/**
 * Created by .
 * User: hal
 * Date: 25-4-11
 * Time: 21:57
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.controller.product.Product', {
    extend: 'Ext.app.Controller',
    alias: 'widget.productcontroller',

    models: [
        'product.Product',
        'product.GenericName',
        'product.BrandName',
        'product.ShapeName',
        'product.UnitName',
        'product.PackageName',
        'product.ProductRoute',
        'product.ProductSubstance'
    ],

    stores: [
        'product.GenericName',
        'product.BrandName',
        'product.ShapeName',
        'product.UnitName',
        'product.PackageName',
        'GenForm.data.ProductSubstanceTestData'//'GenForm.store.product.ProductSubstanceStore'
    ],

    views: [
        'product.ProductWindow',
        'product.ProductForm',
        'product.ProductSubstanceForm',
        'product.ProductSubstanceGrid',
        'product.BrandWindow',
        'product.BrandForm',
        'product.GenericWindow',
        'product.GenericForm',
        'product.ShapeWindow',
        'product.ShapeForm',
        'product.PackageWindow',
        'product.PackageForm',
        'product.UnitWindow',
        'product.UnitForm'
    ],

    init: function() {
        var me = this;

        me.control({
            'productwindow > toolbar button[action=save]': {
                click: me.saveProduct
            },
            'productwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'productwindow > productform > fieldset > combobox[name="GenericName"]':
            {
                editoradd: me.editOrAddGeneric
            },
            'productwindow > productform > fieldset > combobox[name="BrandName"]':
            {
                editoradd: me.editOrAddBrand
            },
            'productwindow > productform > fieldset > combobox[name="ShapeName"]':
            {
                editoradd: me.editOrAddShape
            },
            'productwindow > productform > fieldset > combobox[name="PackageName"]':
            {
                editoradd: me.editOrAddPackage
            },
            'productwindow > productform > fieldset > combobox[name="UnitName"]':
            {
                editoradd: me.editOrAddUnit
            },
            'brandwindow > toolbar button[action=save]': {
                click: me.saveBrand
            },
            'brandwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'genericwindow > toolbar button[action=save]': {
                click: me.saveGeneric
            },
            'genericwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'shapewindow > toolbar button[action=save]': {
                click: me.saveShape
            },
            'shapewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'packagewindow > toolbar button[action=save]': {
                click: me.savePackage
            },
            'packagewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'unitwindow > toolbar button[action=save]': {
                click: me.saveUnit
            },
            'unitwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            }
        });

        me.callParent(arguments);
    },

    editOrAddGeneric: function () {
        var me = this;
        me.getGenericWindow().show();
    },

    showBrandWindow: function () {
        var me = this;

        me.getBrandWindow().show();
    },

    createBrandWindow: function () {
        return Ext.create(this.getProductBrandWindowView());
    },

    editOrAddBrand: function () {
        var me = this;
        me.showBrandWindow();
    },

    editOrAddShape: function () {
        var me = this;
        me.getShapeWindow().show();
    },

    editOrAddPackage: function () {
        var me = this;
        me.getPackageWindow().show();
    },

    editOrAddUnit: function () {
        var me = this;
        me.getUnitWindow().show();
    },

    showProductWindow: function () {
        this.getProductWindow().show();
    },

    getProductWindow: function () {
        var me = this, form;

        form = me.createProductWindow();
        me.loadEmptyProduct(form);
        return form;
    },

    getBrandWindow: function () {
        var me = this, form;

        form = me.createBrandWindow();
        me.loadEmptyBrand(form);
        return form;
    },

    getGenericWindow: function () {
        var me = this, form;

        form = me.createGenericWindow();
        me.loadEmptyGeneric(form);
        return form;
    },

    getShapeWindow: function () {
        var me = this, form;

        form = me.createShapeWindow();
        me.loadEmptyShape(form);
        return form;
    },

    getPackageWindow: function () {
        var me = this, form;

        form = me.createPackageWindow();
        me.loadEmptyPackage(form);
        return form;
    },

    getUnitWindow: function () {
        var me = this, form;

        form = me.createUnitWindow();
        me.loadEmptyUnit(form);
        return form;
    },

    createProductWindow: function () {
        return Ext.create(this.getProductProductWindowView());    
    },

    createGenericWindow: function () {
        return Ext.create(this.getProductGenericWindowView());
    },

    createShapeWindow: function () {
        return Ext.create(this.getProductShapeWindowView());
    },

    createPackageWindow: function () {
        return Ext.create(this.getProductPackageWindowView());
    },

    createUnitWindow: function () {
        return Ext.create(this.getProductUnitWindowView());
    },

    getProduct: function (button) {
        return button.up('panel').down('form').getProduct()
    },

    getBrand: function (button) {
        return button.up('panel').down('form').getBrand();
    },

    getGeneric: function (button) {
        return button.up('panel').down('form').getGeneric();
    },

    getShape: function (button) {
        return button.up('panel').down('form').getShape();
    },

    getPackage: function (button) {
        return button.up('panel').down('form').getPackage();
    },

    getUnit: function (button) {
        return button.up('panel').down('form').getUnit();
    },

    saveProduct: function (button) {
        var me = this,
            product = me.getProduct(button);

        Product.SaveProduct(product.data, {scope: me, callback: me.onProductSaved});
    },

    saveBrand: function (button) {
        var me = this,
            brand = me.getBrand(button);

        Product.AddNewBrand(brand.data, {scope: me, callback:me.onBrandSaved});
    },

    saveGeneric: function (button) {
        var me = this,
            generic = me.getGeneric(button);

        Product.AddNewGeneric(generic.data, {scope: me, callback:me.onGenericSaved});
    },

    saveShape: function (button) {
        var me = this,
            shape = me.getShape(button);

        Product.AddNewShape(shape.data, {scope: me, callback:me.onShapeSaved});
    },

    savePackage: function (button) {
        var me = this,
            package = me.getPackage(button);

        Product.AddNewPackage(package.data, {scope: me, callback:me.onPackageSaved});
    },

    saveUnit: function (button) {
        var me = this,
            unit = me.getUnit(button);

        Product.AddNewUnit(unit.data, {scope: me, callback:me.onUnitSaved});
    },

    onProductSaved: function (result) {
        if (result.success) {
            Ext.MessageBox.alert('Product saved: ', result.data.ProductName);
        } else {
            Ext.MessageBox.alert('Product could not be saved: ', result.message);
        }
    },

    onBrandSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('brandwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Brand saved: ' + result.data.BrandName);
            me.addBrandToStore(result.data.BrandName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Brand could not be saved: ' + result.message);
        }
    },

    onGenericSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('genericwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Generic saved: ', result.data.GenericName);
            me.addGenericToStore(result.data.GenericName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Generic could not be saved: ', result.message);
        }
    },

    onShapeSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('shapewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Shape saved: ', result.data.ShapeName);
            me.addShapeToStore(result.data.ShapeName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Shape could not be saved: ', result.message);
        }
    },

    onPackageSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('packagewindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Package saved: ', result.data.PackageName);
            me.addPackageToStore(result.data.PackageName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Package could not be saved: ', result.message);
        }
    },

    onUnitSaved: function (result) {
        var me = this,
            window = Ext.ComponentQuery.query('unitwindow')[0];

        if (result.success) {
            Ext.MessageBox.alert('Unit saved: ', result.data.UnitName);
            me.addUnitToStore(result.data.UnitName);
            if (window) window.close();
        } else {
            Ext.MessageBox.alert('Unit could not be saved: ', result.message);
        }
    },

    addBrandToStore: function (brand) {
        var me = this,
            store = me.getBrandStore();

        store.add({BrandName: brand});
    },

    addGenericToStore: function (generic) {
        var me = this,
            store = me.getGenericStore();

        store.add({GenericName: generic});
    },

    addShapeToStore: function (shape) {
        var me = this,
            store = me.getShapeStore();

        store.add({ShapeName: shape});
    },

    addPackageToStore: function (package) {
        var me = this,
            store = me.getPackageStore();

        store.add({PackageName: package});
    },

    addUnitToStore: function (unit) {
        var me = this,
            store = me.getUnitStore();

        store.add({UnitName: unit});
    },

    getBrandStore: function () {
        var me = this;
        return me.getProductBrandNameStore();
    },

    getGenericStore: function () {
        var me = this;
        return me.getProductGenericNameStore();
    },

    getShapeStore: function () {
        var me = this;
        return me.getProductShapeNameStore();
    },

    getPackageStore: function () {
        var me = this;
        return me.getProductPackageNameStore();
    },

    getUnitStore: function () {
        var me = this;
        return me.getProductUnitNameStore();
    },

    showCancelMessage: function () {
        Ext.MessageBox.alert('Cancel Product');
    },

    createEmptyProduct: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Product');
    },

    createEmptyBrand: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.BrandName');
    },

    createEmptyShape: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.ShapeName');
    },

    createEmptyGeneric: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.GenericName');
    },

    createEmptyPackage: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.PackageName');
    },

    createEmptyUnit: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.UnitName');
    },

    loadEmptyProduct: function (window) {
        window.loadWithProduct(this.createEmptyProduct());
    },

    loadEmptyGeneric: function (window) {
        window.loadWithGeneric(this.createEmptyGeneric());
    },

    loadEmptyShape: function (window) {
        window.loadWithShape(this.createEmptyShape());
    },

    loadEmptyPackage: function (window) {
        window.loadWithPackage(this.createEmptyPackage());
    },

    loadEmptyUnit: function (window) {
        window.loadWithUnit(this.createEmptyUnit());
    },

    loadEmptyBrand: function (window) {
        window.loadWithBrand(this.createEmptyBrand());
    }

});