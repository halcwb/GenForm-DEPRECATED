//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.controller.product.Product', {
    extend: 'Ext.app.Controller',
    alias: 'widget.productcontroller',

    mixins: {
        productHandler: 'GenForm.controller.mixin.ProductHandler'
    },

    models: [
        'product.Product',
        'product.Shape',
        'product.Unit',
        'product.Package',
        'product.Route',
        'product.ProductSubstance',
        'product.ProductRoute',
        'product.Substance',
        'product.SubstanceGroup',
        'product.UnitGroup'
    ],

    stores: [
        'product.Shape',
        'product.Package',
        'product.ProductSubstance',
        'product.Substance',
        'product.SubstanceGroup',
        'product.ProductRoute',
        'product.Route',
        'product.Unit',
        'product.UnitGroup'
    ],

    views: [
        'product.ProductWindow',
        'product.ProductForm',
        'product.ProductSubstanceForm',
        'product.ProductSubstanceGrid',
        'product.BrandNameWindow',
        'product.BrandNameForm',
        'product.GenericNameWindow',
        'product.GenericNameForm',
        'product.ShapeWindow',
        'product.ShapeForm',
        'product.PackageWindow',
        'product.PackageForm',
        'product.UnitWindow',
        'product.SubstanceUnitWindow',
        'product.UnitForm',
        'product.SubstanceUnitForm',
        'product.ProductGrid',
        'product.ProductSubstanceWindow',
        'product.SubstanceWindow',
        'product.SubstanceForm'
    ],

    init: function() {
        var me = this;

        //noinspection JSUnusedGlobalSymbols
        me.control({
            'panel[region=west]': {
                render: me.onRegionWestRendered
            },
            'panel[title=Menu] > buttongroup > button[text=Nieuw Artikel]': {
                click: me.showProductWindow
            },
            'button[action=addGenericName]': {
                click: me.onAddGenericName
            },
            'button[action=editGenericName]': {
                click: me.onEditGenericName
            },
            'productwindow > toolbar button[action=save]': {
                click: me.saveProduct
            },
            'productwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'productform button[text=Voeg stof toe]': {
                click: me.showProductSubstanceWindow
            },
            'button[action=addBrandName]':
            {
                click: me.onAddBrandName
            },
            'button[action=editBrandName]':
            {
                click: me.onEditBrandName
            },
            'button[action=addShape]':
            {
                click: me.onAddShape
            },
            'button[action=editShape]':
            {
                click: me.onEditShape
            },
            'button[action=addPackage]':
            {
                click: me.onAddPackage
            },
            'button[action=editPackage]':
            {
                click: me.onEditPackage
            },
            'button[action=addProductUnit]':
            {
                click: me.onAddUnit
            },
            'button[action=editProductUnit]':
            {
                click: me.onEditUnit
            },
            'button[action=addProductSubstance]':
            {
                click: me.onAddProductSubstance
            },
            'button[action=editSubstance]':
            {
                click: me.onEditSubstance
            },
            'button[action=addSubstance]':
            {
                click: me.onAddSubstance
            },
            'button[action=editSubstanceUnit]':
            {
                click: me.onEditSubstanceUnit
            },
            'button[action=addSubstanceUnit]':
            {
                click: me.onAddSubstanceUnit
            },
            'brandnamewindow > toolbar button[action=save]': {
                click: me.saveBrand
            },
            'brandnamewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'genericnamewindow > toolbar button[action=save]': {
                click: me.saveGeneric
            },
            'genericnamewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'shapewindow > toolbar button[action=save]': {
                click: me.onSaveShape
            },
            'shapewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'packagewindow > toolbar button[action=save]': {
                click: me.onSavePackage
            },
            'packagewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'unitwindow > toolbar button[action=save]': {
                click: me.saveUnit
            },
            'unitwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'substanceunitwindow > toolbar button[action=save]': {
                click: me.saveSubstanceUnit
            },
            'substanceunitwindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            },
            'substancewindow > toolbar button[action=save]': {
                click: me.saveSubstance
            },
            'substancewindow > toolbar button[action=cancel]': {
                click: me.showCancelMessage
            }
        });
    },

    onRegionWestRendered: function (westpanel) {
        westpanel.add([ { xtype: 'productgrid'} ]);
    },

    showCancelMessage: function () {
        Ext.MessageBox.alert('Cancel Product');
    },

    addKeyValueToStore: function (store, key, value) {
        var keyValue = Ext.create('GenForm.model.common.KeyValue', {
            Key: key,
            Value: value
        });

        store.add(keyValue);
    }

});