//noinspection JSUnusedGlobalSymbols
Ext.define('GenForm.controller.product.Product', {
    extend: 'Ext.app.Controller',
    alias: 'widget.productcontroller',

    mixins: {
        productHandler: 'GenForm.controller.mixin.ProductHandler'
    },

    models: [
        'product.Product',
        'product.GenericName',
        'product.BrandName',
        'product.ShapeName',
        'product.UnitName',
        'product.PackageName',
        'product.ProductRoute',
        'product.ProductSubstance',
        'product.SubstanceName',
    ],

    stores: [
        'product.GenericName',
        'product.BrandName',
        'product.ShapeName',
        'product.UnitName',
        'product.PackageName',
        'product.ProductSubstance',
        'product.SubstanceName',
        'product.SubstanceUnitName'
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
        'product.UnitForm',
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
            'panel[title=Menu] > buttongroup > button[text=Nieuw Generiek]': {
                click: me.editOrAddGeneric
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
            'productsubstancewindow combobox[name="SubstanceName"]':
            {
                editoradd: me.editOrAddSubstance
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
    }

});