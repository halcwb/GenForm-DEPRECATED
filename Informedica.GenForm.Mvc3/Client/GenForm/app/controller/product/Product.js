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
        'component.EditableComboBox',
        'product.ProductWindow',
        'product.ProductForm',
        'product.ProductSubstanceForm',
        'product.ProductSubstanceGrid',
        'product.BrandWindow',
        'product.BrandForm'
    ],

    productWindow: null,
    
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
            }
        });

    },

    editOrAddGeneric: function () {
        Ext.MessageBox.alert('Edit or add Generic');
    },

    showBrandWindow: function () {
        var me = this,
            window = me.createBrandWindow();
        
        window.show();
    },

    createBrandWindow: function () {
        return Ext.create(this.getProductBrandWindowView());
    },

    editOrAddBrand: function () {
        var me = this;
        me.showBrandWindow();
    },

    editOrAddShape: function () {
        Ext.MessageBox.alert('Edit or add Shape');
    },

    editOrAddPackage: function () {
        Ext.MessageBox.alert('Edit or add Package');
    },

    editOrAddUnit: function () {
        Ext.MessageBox.alert('Edit or add Unit');
    },

    showProductWindow: function () {
        this.getProductWindow().show();
    },

    getProductWindow: function () {
        if(!this.productWindow) {
            this.productWindow = this.createProductWindow();
            this.loadEmptyProduct(this.productWindow);
        }
        return this.productWindow;
    },

    createProductWindow: function () {
        return Ext.create(this.getProductProductWindowView());    
    },

    getProduct: function (button) {
        return button.up('panel').down('form').getProduct()
    },

    saveProduct: function (button) {
        var me = this,
            product = me.getProduct(button);

        Product.SaveProduct(product.data, {scope: me, callback: me.onProductSaved});
    },

    saveBrand: function (button) {
        var me = this;
        Ext.MessageBox.alert('Brand is saved?');
    },

    onProductSaved: function (result) {
        if (result.success) {
            Ext.MessageBox.alert('Product saved: ' + result.data.ProductName);
        } else {
            Ext.MessageBox.alert('Product could not be saved: ' + result.message);
        }
    },

    showCancelMessage: function () {
        Ext.MessageBox.alert('Cancel Product');
    },

    createEmptyProduct: function () {
        return Ext.ModelManager.create({}, 'GenForm.model.product.Product');
    },

    loadEmptyProduct: function (window) {
        window.loadWithProduct(this.createEmptyProduct());
    }

});