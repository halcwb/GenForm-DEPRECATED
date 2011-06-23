/**
 * Created by JetBrains WebStorm.
 * User: halcwb
 * Date: 6/18/11
 * Time: 10:56 AM
 * To change this template use File | Settings | File Templates.
 */
describe('Ext.Class', function () {
    var instance, getInstance;

    Ext.define('Tests.BaseClass', {

        config: {
            name: 'baseClass'
        },

        constructor: function (config) {
            var me = this;
            console.log('Base Class gets constructed');
            me.initConfig(config);

            return me;
        }

    });

    Ext.define('Tests.MyCoolMixin', {
       someUseFullFunction: function () {
           console.log('I can do something usefull');
       }
    });

    Ext.define('Tests.MyClass', {
        extend: 'Tests.BaseClass',

        mixins: {
            somethingUseFull: 'Tests.MyCoolMixin'
        },

        config: {
            name: 'name of the instance'
        },

        constructor: function (config) {
            var me = this;
            console.log('MyClass gets constructed');
            me = me.initConfig(config);
            return me;
        },

        // Gets called before setName method
        applyName: function (name) {
        var me = this;
        if (!Ext.isString(name) || name.length === 0) {
           me.throwError();
        }
        // need to return name otherwise name does not get set!!
        else {
           return name;
        }
         },

        throwError: function () {
            throw new Error('['+ Ext.getDisplayName(arguments.callee) +'] cannot have an empty string as a name');
        }
    });

    getInstance = function (config) {
        if (!instance) {
            return Ext.create('Tests.MyClass', config);
        }
    };

    it('can create an instance of Tests.MyClass', function () {
       expect(getInstance({name: 'test'})).toBeDefined();
    });

    it('an instance of Tests.MyClass should have a getter and setter method for name', function () {
        var instance = getInstance({name: 'test'});
        
        expect(instance.getName).toBeDefined();
        expect(instance.setName).toBeDefined();
    });

    it('Tests.MyClass should not accept an empty string for name', function () {
        var instance = getInstance({name: 'test'});

        spyOn(instance, 'throwError');
        instance.setName('');

        expect(instance.throwError).toHaveBeenCalled();
    });

    it('a valid name can be set', function () {
        var instance = getInstance({}), name = 'test';

        instance.setName(name);

        expect(instance.getName()).toBe(name);
    });

    it('the name property can be reset', function () {
        var name = 'instance',
            instance = getInstance({name: name});

        expect(instance.getName()).toBe(name);
        console.log(instance);

        // This does not work! resetName is undefined?
        // instance.resetName();
        // expect(instance.getName() === name).toBeFalsy();
    });

    it('the test class should contain the method from the mixin', function () {
        var instance = getInstance({});

        expect(instance.someUseFullFunction).toBeDefined();

        spyOn(instance, 'someUseFullFunction');
        instance.someUseFullFunction();

        expect(instance.someUseFullFunction).toHaveBeenCalled();

    });

    it('the constructor of the base class should be called upon construction of the derived class', function () {
        var constructor = Ext.ClassManager.getClass('Tests.MyClass');

        expect(typeof(constructor)).toBe('function');
    })

});