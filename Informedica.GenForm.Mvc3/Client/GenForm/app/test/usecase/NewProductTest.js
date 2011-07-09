Ext.define('GenForm.test.usecase.NewProductTest', {
    describe: 'NewProductTest tests that',

    tests: function () {
        console.log(this);
        var me = this,
            queryHelper = Ext.create('GenForm.test.util.QueryHelper'),
            messageChecker = Ext.create('GenForm.test.util.MessageChecker'),
            domClicker = Ext.create('GenForm.test.util.DomClicker'),
            message = '',
            waitingTime = 500,
            product = 'dopamine Dynatra infusievloeistof 5 mL ampul',
            code = '123456',
            generic = 'dopamine',
            brand = 'Dynatra',
            shape = 'infusievloeistof',
            quantity = 5,
            unit = 'mL',
            package = 'ampul';

        me.getNewProductButton = function () {
            return queryHelper.getButton('panel[title=Menu]','Nieuw Artikel');
        };

        me.clickNewProductButton = function () {
            queryHelper.clickButton(me.getNewProductButton());
        };

        me.clickSaveGenericButton = function () {
            queryHelper.clickButton(queryHelper.getButton('genericwindow', 'Opslaan'));
        };

        me.clickSaveBrandButton = function () {
            queryHelper.clickButton(queryHelper.getButton('brandwindow', 'Opslaan'));
        };

        me.clickSaveShapeButton = function () {
            queryHelper.clickButton(queryHelper.getButton('shapewindow', 'Opslaan'));
        };

        me.clickSaveUnitButton = function () {
            queryHelper.clickButton(queryHelper.getButton('unitwindow', 'Opslaan'));
        };

        me.clickSavePackageButton = function () {
            queryHelper.clickButton(queryHelper.getButton('packagewindow', 'Opslaan'));
        };

        me.clickSaveProductButton = function () {
            queryHelper.clickButton(queryHelper.getButton('productwindow', 'Opslaan'));
        };

        me.getNewGenericButton = function () {
            return queryHelper.getButton('panel[title=Menu]', 'Nieuw Generiek');
        };

        me.getProductWindow = function () {
            return Ext.ComponentQuery.query('productwindow')[0];
        };

        me.getGenericWindow = function () {
            return Ext.ComponentQuery.query('genericwindow')[0];
        };

        me.getBrandWindow = function () {
            return Ext.ComponentQuery.query('brandwindow')[0];
        };

        me.getShapeWindow = function () {
            return Ext.ComponentQuery.query('shapewindow')[0];
        };

        me.getUnitWindow = function () {
            return Ext.ComponentQuery.query('unitwindow')[0];
        };

        me.getPackageWindow = function () {
            return Ext.ComponentQuery.query('packagewindow')[0];
        };

        me.getProductName = function () {
            return queryHelper.getFormTextField('productform', 'ProductName');
        };

        me.setProductName = function (name) {
            queryHelper.setFormField(me.getProductName(), name);
        };
        
        me.getProductCode = function () {
            return queryHelper.getFormTextField('productform', 'ProductCode');
        };

        me.setProductCode = function (name) {
            queryHelper.setFormField(me.getProductCode(), name);
        };

        me.getGenericName = function () {
            return queryHelper.getFormTextField('genericwindow', 'GenericName');
        };

        me.setGenericName = function (name) {
            queryHelper.setFormField(me.getGenericName(), name);
        };

        me.getBrandName = function () {
            return queryHelper.getFormTextField('brandwindow', 'BrandName');
        };

        me.setBrandName = function (name) {
            queryHelper.setFormField(me.getBrandName(), name);
        };

        me.getShapeName = function () {
            return queryHelper.getFormTextField('shapewindow', 'ShapeName');
        };

        me.setShapeName = function (name) {
            queryHelper.setFormField(me.getShapeName(), name);
        };

        me.getUnitName = function () {
            return queryHelper.getFormTextField('unitwindow', 'UnitName');
        };

        me.setUnitName = function (name) {
            queryHelper.setFormField(me.getUnitName(), name);
        };

        me.getPackageName = function () {
            return queryHelper.getFormTextField('packagewindow', 'PackageName');
        };

        me.setPackageName = function (name) {
            queryHelper.setFormField(me.getPackageName(), name);
        };

        me.setGenericCombo = function (name) {
            queryHelper.setFormField(me.getGenericCombo(), name);
        };

        me.getGenericCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'GenericName');
        }

        me.setBrandCombo = function (name) {
            queryHelper.setFormField(me.getBrandCombo(), name);
        };

        me.getBrandCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'BrandName');
        };

        me.setShapeCombo = function (name) {
            queryHelper.setFormField(me.getShapeCombo(), name);
        };

        me.getShapeCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'ShapeName');
        };

        me.setUnitCombo = function (name) {
            queryHelper.setFormField(me.getUnitCombo(), name);
        };

        me.getUnitCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'UnitName');
        };

        me.setPackageCombo = function (name) {
            queryHelper.setFormField(me.getPackageCombo(), name);
        };

        me.getPackageCombo = function () {
            return queryHelper.getFormComboBox('productwindow', 'PackageName');
        };

        me.getQuantity = function () {
            return queryHelper.getFormTextField('productwindow', 'Quantity');
        };

        me.setQuantity = function(quantity) {
            queryHelper.setFormField(me.getQuantity(), quantity);
        };

        me.checkMessage = function () {
            if (messageChecker.checkMessage(message)) {
                message = "";
                return true;
            } else {
                return false;
            }

        };

        me.clickAddGeneric = function () {
            domClicker.click(me.getAddButton('GenericName'));
        };

        me.clickAddBrand = function () {
            domClicker.click(me.getAddButton('BrandName'))
        };

        me.clickAddShape = function () {
            domClicker.click(me.getAddButton('ShapeName'))
        };

        me.clickAddUnit = function () {
            domClicker.click(me.getAddButton('UnitName'))
        };

        me.clickAddPackage = function () {
            domClicker.click(me.getAddButton('PackageName'))
        };

        me.getAddButton = function (name) {
            return Ext.ComponentQuery.query('productwindow combobox[name=' + name +']')[0].triggerWrap.dom.childNodes[2]
        };

        it('User sees an empty Product window', function () {
            me.clickNewProductButton();
            expect(me.getProductWindow()).toBeDefined();
        });

        it('The product window is empty', function () {
            expect(me.getProductWindow().isEmpty()).toBeTruthy();
        });

        it('User can enter a productname', function () {
            me.setProductName(product);

            expect(me.getProductName().value).toBe(product)
        });

        it('User can enter a product quantity', function () {
            me.setQuantity(quantity);
            expect(me.getQuantity().value).toBe(quantity)
        });

        it('User can open up a window to add a new generic', function () {
            me.clickAddGeneric();
            expect(me.getGenericWindow()).toBeDefined();
        });

        it('User can enter a name for the new generic', function () {
            me.setGenericName(generic);
            expect(me.getGenericName().value).toBe(generic);
        });

        it('A new generic with a valid name can be saved', function () {
            message = generic;
            me.clickSaveGenericButton();
            waitsFor(me.checkMessage, "response of generic save", waitingTime);
        });

        it('The new generic can be set for the product', function () {
            me.setGenericCombo(generic);
            expect(me.getGenericCombo().value).toBe(generic);
        });

        it('User can open up a window to add a new Brand', function () {
            me.clickAddBrand();
            expect(me.getBrandWindow()).toBeDefined();
        });

        it('User can enter a name for the new brand', function (){
            me.setBrandName(brand);
            expect(me.getBrandName().value).toBe(brand);
        });

        it('User can save the new brand', function () {
            message = brand;
            me.clickSaveBrandButton();
            waitsFor(me.checkMessage, 'response of brand save', waitingTime);
        });

        it('The new brand can be set for the product', function () {
            me.setBrandCombo(brand);
            expect(me.getBrandCombo().value).toBe(brand);
        });

        it('User can open up a window to add a Shape', function () {
            me.clickAddShape();
            expect(me.getShapeWindow()).toBeDefined();
        });

        it('User can enter a name for the new Shape', function () {
            me.setShapeName(shape);
            expect(me.getShapeName().value).toBe(shape);
        });

        it('User can save the new Shape', function () {
            message = shape;
            me.clickSaveShapeButton();
            waitsFor(me.checkMessage, 'response of shape save', waitingTime);
        });

        it('The new Shape can be set for the product', function () {
            me.setShapeCombo(shape);
            expect(me.getShapeCombo().value).toBe(shape);
        });

        it('User can open up a window to add a Unit', function () {
            me.clickAddUnit();
            expect(me.getUnitWindow()).toBeDefined();
        });

        it('User can enter a name for unit', function () {
            me.setUnitName(unit);
            expect(me.getUnitName().value).toBe(unit);
        });

        it('User can save the new unit', function () {
            message = unit;
            me.clickSaveUnitButton();
            waitsFor(me.checkMessage, 'response of unit save', waitingTime);
        });

        it('The new unit can be set for the product', function () {
            me.setUnitCombo(unit);
            expect(me.getUnitCombo().value).toBe(unit);
        });

        it('User can open a window to add a Package', function () {
            me.clickAddPackage();
            expect(me.getPackageWindow()).toBeDefined();
        });

        it('User can enter a Package name', function () {
            me.setPackageName(package);
            expect(me.getPackageName().value).toBe(package);
        });

        it('User can save the new Package', function () {
            message = package;
            me.clickSavePackageButton();
            waitsFor(me.checkMessage, 'response of package save', waitingTime);
        });

        it('The new package can be set for the product', function () {
            me.setPackageCombo(package);
            expect(me.getPackageCombo().value).toBe(package);
        });

        it('User can set the product code', function () {
            me.setProductCode(code);
            expect(me.getProductCode().value).toBe(code);
        });

        it('User can save the filled in Product', function () {
            message = product;
            me.clickSaveProductButton();
            waitsFor(me.checkMessage, 'response of product save', waitingTime);
        });

        it('After saving the product, the new product window is closed', function () {
            var window = me.getProductWindow();
            expect(window).toBeUndefined();
        });
        
    }
});

